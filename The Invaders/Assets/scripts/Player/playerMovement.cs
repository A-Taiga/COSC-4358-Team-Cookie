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

    

    protected override void Awake() {
        base.Awake();
        mainCam = Camera.main;
        //anim = GetComponent<Animator>();
    }


    private void Update() {




        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        run = Input.GetButton("Jump");
        HandleMovement(moveX, moveY, run);

        // Debug.Log("X: " + moveDelta.x + "Y: " + moveDelta.y);

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
    void HandlePlayerAnimation(float x, float y) {}

}
