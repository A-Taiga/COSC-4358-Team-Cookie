using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
public class playerMovement : characterMovement
{
    private float moveX, moveY;
    private bool run;

    private Camera mainCam;
    private Vector2 mousePos;
    private Vector3 tempScale;

    public Animator animator;

    public Vector3 screenPosition;
    public Vector3 worldPosition;


    private bool isMoving = false;

    protected override void Awake() {

        base.Awake();
        mainCam = Camera.main;
        // animator = GetComponent<Animator>();
    }



    private void Update() {

        
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        run = Input.GetButton("Jump");
        HandleMovement(moveX, moveY, run);

        // Debug.Log("X: " + moveDelta.x + "Y: " + moveDelta.y);

        if(moveX != 0 || moveY != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        /* player movement animation */
        animator.SetFloat("Horizontal", moveDelta.x);
        animator.SetFloat("Vertical", moveDelta.y);
        animator.SetFloat("Speed", moveDelta.sqrMagnitude);

         /* get position of mouse relative to the world */
        screenPosition = Input.mousePosition;
        worldPosition = mainCam.ScreenToWorldPoint(screenPosition);


        // if(Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("MOUSE CLICK");
        //     if(GameObject.Find("Player").transform.position.x > worldPosition.x)
        //     {
        //         animator.SetBool("AttackLeft", true);
        //     }
        // }


        //Debug.Log("PLAYER: " + GameObject.Find("Player").transform.position + "MOUSE: " + worldPosition);

        if(Input.GetMouseButtonDown(0))
        {
            if(GameObject.Find("Player").transform.position.x > worldPosition.x)
            {
                animator.SetBool("AttackLeft", true);
            }
            else
            {
                animator.SetBool("AttackRight", true);
            }
        }
        else
        {
            animator.SetBool("AttackLeft", false);
            animator.SetBool("AttackRight", false);
        }
    }

    public  void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Orb"))
        {
            print("Orb");
        }
    }

    void HandlePlayerAnimation(float x, float y) {}

    public bool getIsMoving() {
        return isMoving;
    }

}
