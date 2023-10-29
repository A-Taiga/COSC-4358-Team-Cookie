using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    //[SerializeField]
    public float FollowDistance = 1f;
    AIAction lastAction;
    public enemyProjectile projectile;
    public Transform launchOffset;

    private Range range;
    public Animator animator;

    private float timeWhenAllowedNextShoot = 0f;
    private float timeBetweenShooting = 1f;

    void Start()
    {
        lastAction = null;
        range = GetComponentInChildren<Range>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(lastAction?.lockAction == true)
        {
            return;
        }

        if(range.shoot == true)
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
                lastAction = action;
                // Instantiate(projectile, launchOffset.position, transform.rotation);
            }
            // if(action?.shoot == true)
            // {
            //     print("SHOOTING");
            //     // Instantiate(projectile, launchOffset.position, transform.rotation);
            // }
        }
        if (lastAction?.desire != 0)
        {
            lastAction?.Execute();
        }
        else 
        {
            animator?.SetFloat("Speed", 0f);
        }
    }
}
