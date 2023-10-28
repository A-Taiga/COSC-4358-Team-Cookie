using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{


    public Animator animator;

    public bool shoot;
    // Start is called before the first frame update
    void Awake()
    {
        shoot = false;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Attack", true);
            shoot = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Attack", false);
            shoot = false;
        }
    }
}
