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
    private Vector3 lookDir;
    private Vector3 tempScale;

    public Animator animator;
    

    protected override void Awake() {
        base.Awake();
        mainCam = Camera.main;
        //anim = GetComponent<Animator>();
    }


    private void FixedUpdate() {


        /* reset Speed parameter of animation */
        animator.SetFloat("Speed",0);
        /* restet North parameter of animation */
        animator.SetBool("North", false);
        animator.SetBool("South", false);



        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        run = Input.GetButton("Jump");
        HandleMovement(moveX, moveY, run);


        // Debug.Log("moveX: " + moveX + " moveY: " + moveY);

        if (moveX != 0)
            animator.SetFloat("Speed", MathF.Abs(moveX));

        if (moveY != 0)
        {
            if(moveY == 1)
                animator.SetBool("North", true);
            else if(moveY == -1)
                animator.SetBool("South", true);
        }
           
            // animator.SetFloat("Speed", MathF.Abs(moveY));
        
        /* handle player direction */
            HandlePlayerTurning(moveX);
    }
    void HandlePlayerTurning(float moveX) 
    {
        if(moveX < 0)
        {
            lookDir = new Vector3(-0.05f, 0.05f, 0.05f);
            transform.localScale = lookDir;
        }
        else if(moveX > 0)
        {
            lookDir = new Vector3(0.05f, 0.05f, 0.05f);
            transform.localScale = lookDir;
        }
    }
    void HandlePlayerAnimation(float x, float y) {}
}
