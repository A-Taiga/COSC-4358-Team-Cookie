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

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        run = Input.GetButton("Jump");
        HandleMovement(moveX, moveY, run);

        Debug.Log("X: " + moveDelta.x + "Y: " + moveDelta.y);
        animator.SetFloat("Horizontal", moveDelta.x);
        animator.SetFloat("Vertical", moveDelta.y);
        animator.SetFloat("Speed", moveDelta.sqrMagnitude);
        

    }
    void HandlePlayerAnimation(float x, float y) {}
}
