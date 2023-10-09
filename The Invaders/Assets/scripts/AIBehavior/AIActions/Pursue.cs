using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : AIAction
{
    protected float speed = 1f / 2;
    private Vector2 playerLocation;



    public override float Desire(RaycastHit2D[] rays)
    {
        this.desire = 0;
        if (!this.collidingPlayer)
        {
            foreach (var ray in rays)
            {
                if (ray.rigidbody != null && ray.rigidbody.tag == TagManager.PLAYER_TAG)
                {
                    this.desire = 1;
                    playerLocation = ray.rigidbody.position;
                    break;
                }
            }
        }
        return this.desire;
    }

    public override void Execute()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, step);
    }
}