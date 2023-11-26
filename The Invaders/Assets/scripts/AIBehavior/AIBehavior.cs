using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    private int health = 100;
    
    //[SerializeField]
    public float FollowDistance = 1f;
    AIAction lastAction;
    public enemyProjectile projectile;
    public Transform launchOffset;
    private Range range;
    public Animator animator;
    private float timeWhenAllowedNextShoot = 0f;
    private float timeBetweenShooting = 1f;

    private FloatingHealthBar healthbar;

    void Start()
    {
        healthbar = GetComponentInChildren<FloatingHealthBar>();
        lastAction = null;
        range = GetComponentInChildren<Range>();
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            print("HEALTH: " + health);
            health -= 20;
            healthbar.UpdateHealthBar(health);
            if (health <= 0)
            {
                Destroy(gameObject);
                if (Player.progress < 2)
                {
                    Player.progress = 2;
                    Player.getPlayerObject()
                        .GetComponentInChildren<PopupMessage>()
                        .ShowPopup("I can now enter the Sunset Bay!", 5f);
                }
            }
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(lastAction?.lockAction == true)
        {
            return;
        }
        if(range && range.shoot == true)
        {
            animator.SetFloat("Speed", 0f);
            if (timeWhenAllowedNextShoot <= Time.time)
            {
                Instantiate(projectile, launchOffset.position, Quaternion.identity);
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
                if(GameObject.Find("Player").GetComponent<Transform>().position.x > transform.position.x)
                {
                    animator.SetBool("AttackRight", true);
                    animator.SetBool("AttackLeft", false);
                }
                else if(GameObject.Find("Player").GetComponent<Transform>().position.x < transform.position.x)
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
}
