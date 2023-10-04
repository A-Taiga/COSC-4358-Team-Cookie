using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [SerializeField] 
    protected float xSpeed = 1f, ySpeed = 0.75f, runMultipler = 2f;

    private Vector3 moveDelta;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private RaycastHit2D movementHit;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    protected virtual void HandleMovement(float x, float y, bool run = false) {

        if (run) { 
            moveDelta = new Vector3(x * xSpeed * runMultipler, y * ySpeed * runMultipler, 0f);
        }
        else
        {
            moveDelta = new Vector3(x * xSpeed, y * ySpeed, 0f);
        }
         
        // Two translations required otherwise collisions will block the entire Vector3.
        movementHit = BoxCastDrawer.BoxCastAndDraw(transform.position, bc.bounds.size, 0f,
            new Vector2(moveDelta.x, 0f), Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask(TagManager.BOUNDARY_TAG));

        if (movementHit.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0f, 0f);
        }

        movementHit = BoxCastDrawer.BoxCastAndDraw(transform.position, bc.bounds.size, 0f,
            new Vector2(0f, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask(TagManager.BOUNDARY_TAG));

        if (movementHit.collider == null)
        {
            transform.Translate(0f, moveDelta.y * Time.deltaTime, 0f);
        }
    }

    public Vector3 GetMoveDelta() {
        return moveDelta;
    }

}
