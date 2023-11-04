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

    void Update()
    {

        if(GameObject.Find("Player").GetComponent<Transform>().position.x > transform.position.x && shoot == true)
        {
            animator.SetBool("AttackRight", true);
            animator.SetBool("AttackLeft", false);
        }
        else if(GameObject.Find("Player").GetComponent<Transform>().position.x < transform.position.x && shoot == true)
        {
            animator.SetBool("AttackRight", false);
            animator.SetBool("AttackLeft", true);
        }
    }
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.transform.position.x > transform.position.x)
                animator.SetBool("AttackRight", true);
            else 
                animator.SetBool("AttackLeft", true);
            shoot = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("AttackRight", false);
            animator.SetBool("AttackLeft", false);
            shoot = false;
        }
    }
}
