using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : AIAction
{
    protected float speed = 1f / 2;
    private Vector2 playerLocation;



    public override float Desire(RaycastHit2D[] rays)
    {

        if (rays.Length == 0)
            this.desire = 0;

        foreach (var ray in rays) {
            if (ray.rigidbody != null && ray.rigidbody.tag == TagManager.PLAYER_TAG)
            {
                this.desire = 1;
                playerLocation = ray.point;
                break;
            }
        }
        return this.desire;
    }

    public override void Execute()
    {
        if (!this.collidingPlayer && Vector3.Distance(transform.position, playerLocation) > 0.25f)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, playerLocation, step);
        }

        this.desire = 0;
    }
}
