using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    [SerializeField] 
    protected float xSpeed = 1f, ySpeed = 0.75f;
    private Vector3 moveDelta;
    protected virtual void Awake() {
        // blockLayer = LayerMask.GetMask(TagManager.BOUNDARY_TAG);
        // Debug.Log("blockLayer: " + blockLayer);
    }

    protected virtual void HandleMovement(float x, float y) {
        moveDelta = new Vector3(x * xSpeed, y * ySpeed, 0f);
        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0f);
    }

    public Vector3 GetMoveDelta() {
        return moveDelta;
    }

}
