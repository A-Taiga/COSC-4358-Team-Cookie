using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehavior : MonoBehaviour, ISaveable
{
    
    //[SerializeField]
    public float FollowDistance = 1f;
    AIAction lastAction;
    public enemyProjectile projectile;
    public Transform launchOffset;
    public Animator animator;
    public string enemyType;
    public float health = 100;
    
    private Range range;
    private float timeWhenAllowedNextShoot = 0f;
    private float timeBetweenShooting = 1f;
    private FloatingHealthBar healthbar;
    private Player player;
    public bool toRespawn = true;

    public GameObject coin;
    
    void Start()
    {
        SaveManager.Instance.LoadData(this);
        healthbar = GetComponentInChildren<FloatingHealthBar>();
        lastAction = null;
        range = GetComponentInChildren<Range>();
        
        if (!toRespawn)
        {
            Destroy(gameObject);
        }
        healthbar.SetMaxHealthBar(health);
        healthbar.UpdateHealthBar(health);
    }

    void Awake()
    {
        player = Player.getPlayerObject().GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {
        print("HEALTH: " + damage);
        health -= damage;
        healthbar.UpdateHealthBar(health);
        if (health <= 0)
        {
            toRespawn = false;
            SaveManager.Instance.SaveData(this);
            if (coin)
            {
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            TakeDamage(20f);
        }
    }*/
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(lastAction?.lockAction == true || PauseManager.isPaused)
        {
            return;
        }
        if(range && range.shoot == true && launchOffset)
        {
            animator.SetFloat("Speed", 0f);
            if (timeWhenAllowedNextShoot <= Time.time)
            {
                Instantiate(projectile, launchOffset.transform.position, Quaternion.identity);
                timeWhenAllowedNextShoot = Time.time + timeBetweenShooting;
            }
        }

        Vector2 fVector = new Vector2(FollowDistance * 2, FollowDistance * 2);
        RaycastHit2D[] hits = new RaycastHit2D[5];
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();

        Physics2D.BoxCast(transform.position, fVector, 0f, Vector2.zero, contactFilter: filter, results: hits);
        //BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, List < RaycastHit2D > results, float distance = Mathf.Infinity);
        BoxCastDrawer.Draw(hits.Last(), transform.position, fVector, 0f, Vector2.zero);

        AIAction[] actions = GetComponents<AIAction>();
        foreach (AIAction action in actions)
        {

            if(action.Desire(hits) > (lastAction?.desire ?? 0))
            {
                animator.SetFloat("Speed", 0f);
                if(player.gameObject.transform.position.x > transform.position.x)
                {
                    animator.SetBool("AttackRight", true);
                    animator.SetBool("AttackLeft", false);
                }
                else if(player.gameObject.transform.position.x < transform.position.x)
                {
                    animator.SetBool("AttackRight", false);
                    animator.SetBool("AttackLeft", true);
                }
                lastAction = action;
            }
        }
        if (lastAction?.desire != 0)
        {
            lastAction?.Execute();
        }
        else 
        {
            animator?.SetFloat("Speed", 0f);
            animator.SetBool("AttackRight", false);
            animator.SetBool("AttackLeft", false);
        }
    }
    
    public void PopulateSaveData(SaveData save)
    {
        if (enemyType.Trim().Length > 0)
        {
            var enemy = new SaveData.EnemyData();
            enemy.enemyName = enemyType;
            enemy.respawnEnemy = toRespawn;
            enemy.enemyHealth = health;
            
            int idx = save.m_EnemyData.RemoveAll(e => { return e.enemyName == enemyType; });
            save.m_EnemyData.Add(enemy);   
        }
    }

    public void LoadFromSaveData(SaveData save)
    {
        if (enemyType.Trim().Length > 0)
        {
            foreach (var enemy in save.m_EnemyData)
            {
                if (enemy.enemyName.Equals(enemyType))
                {
                    toRespawn = enemy.respawnEnemy;
                    health = enemy.enemyHealth;
                    break;
                }
            }
        }
    }
}
