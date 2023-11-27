using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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

    private int atkCombo;
    public AudioSource[] attackSounds;
    public ParticleSystem runParticles;

    private bool movementLock = false;
    private bool isMoving = false;
    public bool isAttacking { private set; get; } = false;

    void volChanger(float v) 
    {
        foreach (var sound in attackSounds)
        {
            sound.volume = v;
        }
    }

    protected override void Awake() {

        base.Awake();
        mainCam = Camera.main;

        foreach (var sound in attackSounds)
        {
            sound.volume = PlayerPrefs.GetFloat("volume", 1f);
        }
        // animator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        Events<VolumeChangeEvent>.Instance.Register(volChanger);
    }

    public void OnDisable()
    {
        Events<VolumeChangeEvent>.Instance.Unregister(volChanger);
    }

    public void UnlockMovement()
    {
        movementLock = false;
    }
    public void LockMovement()
    {
        movementLock = true;
        runParticles.Clear();
        runParticles.Pause();
        isMoving = false;
    }

    private void Update() 
    {
        if(PauseManager.isPaused || movementLock)
        {
            return;
        }
        
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        run = Input.GetButton("Jump") && isMoving;
        HandleMovement(moveX, moveY, run);

        // Debug.Log("X: " + moveDelta.x + "Y: " + moveDelta.y);

        if(moveX != 0 || moveY != 0)
        {
            isMoving = true;
            if(run && !runLocked)
            {
                runParticles.Play();
            } 
            else
            {
                runParticles.Clear();
                runParticles.Pause();
            }
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
        
        if(isAttacking && 
           (!animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_left") 
            && !animator.GetCurrentAnimatorStateInfo(0).IsName("player_attack_right")))
        {
            isAttacking = false;
        } 

        //Debug.Log("PLAYER: " + GameObject.Find("Player").transform.position + "MOUSE: " + worldPosition);

        if(Input.GetMouseButtonDown(0))
        {
            if(GameObject.Find("Player").transform.position.x > worldPosition.x)
            {
                AttackLeft();
            }
            else
            {
                AttackRight();
            }
            attackSounds[atkCombo++]?.Play();
            if (atkCombo == attackSounds.Length)
            {
                atkCombo = 0;
            }

            isAttacking = true;
        }
        else
        {
            animator.SetBool("AttackLeft", false);
            animator.SetBool("AttackRight", false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
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

    void AttackLeft()
    {
        animator.SetBool("AttackLeft", true);
        var enemyHit = BoxCastDrawer.BoxCastAndDraw(bc.bounds.center, bc.bounds.size, 0f,
            Vector2.left, 0.1f,
            LayerMask.GetMask(TagManager.ENEMY_TAG));
        if (enemyHit)
        {
            Debug.Log("WE HIT LEFT");
            enemyHit.transform.gameObject.GetComponent<AIBehavior>().TakeDamage(20f);
        }
    }

    void AttackRight()
    {
        animator.SetBool("AttackRight", true);
        var enemyHit = BoxCastDrawer.BoxCastAndDraw(bc.bounds.center, bc.bounds.size, 0f,
            Vector2.right, 0.1f,
            LayerMask.GetMask(TagManager.ENEMY_TAG));
        if (enemyHit)
        {
            Debug.Log("WE HIT RIGHT");
            enemyHit.transform.gameObject.GetComponent<AIBehavior>().TakeDamage(20f);
        }
    }

}
