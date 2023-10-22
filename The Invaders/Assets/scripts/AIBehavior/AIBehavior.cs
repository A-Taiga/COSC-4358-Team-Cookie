using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{

    //[SerializeField]
    public GameObject owner;
    public float FollowDistance = 1f;
    public float ActionFrequency = 0f;

    protected float lastActionTime = 0f;
    protected AIAction lastAction;

    void Start()
    {
        lastAction = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(lastAction?.lockAction == true || lastActionTime + ActionFrequency > Time.time)
        {
            return;
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
            }
        }
        if (lastAction?.desire != 0)
        {
            lastAction?.Execute();
            lastActionTime = Time.time;
        }
    }
}
