using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : characterMovement
{
    private float moveX, moveY;
    private bool run;

    private Camera mainCam;
    private Vector2 mousePos;
    private Vector2 lookDir;
    private Vector3 tempScale;

    private Animator anim;

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
    }

    void HandlePlayerTurning() {}
    void HandlePlayerAnimation(float x, float y) {}
}
