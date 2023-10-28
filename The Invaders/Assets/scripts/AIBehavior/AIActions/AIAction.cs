using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{

    private int health = 100;
    public float desire { get; protected set; }
    public bool lockAction { get; protected set; }

    public abstract float Desire(RaycastHit2D[] rays);

    protected BoxCollider2D cc;
    protected Rigidbody2D rb;
    protected bool collidingPlayer;

    void Awake()
    {
        collidingPlayer = false;
        lockAction = false;
        cc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            print("HEALTH: " + health);
            health -= 10;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {


        // if(collision.gameObject.tag == "Weapon")
        // {
        //     print("HEALTH: " + health);
        //     health -= 10;
        //     if(health <= 0)
        //     {
        //         Destroy(gameObject);
        //     }
        // }


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
