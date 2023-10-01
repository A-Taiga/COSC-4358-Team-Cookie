using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    public float desire;

    public abstract float Desire(RaycastHit2D[] rays);


    protected BoxCollider2D cc;
    protected Rigidbody2D rb;
    protected bool collidingPlayer;

    void Awake()
    {
        cc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            collidingPlayer = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            collidingPlayer = false;
        }
    }

    public virtual void Execute()
    {
        Debug.Log("AIAction.Execute() called");
    }
}
