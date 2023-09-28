using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private float boundX = 0.3f, boundY = 0.15f;
    private Vector3 deltaPos;
    private float deltaX, deltaY;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void LateUpdate() {

        if(!playerTransform) {
            return;
        }

        deltaPos = Vector3.zero;

        deltaX = playerTransform.position.x - transform.position.x;
        deltaY = playerTransform.position.y - transform.position.y;

        if (deltaX > boundX || deltaX < -boundX) {
            if (transform.position.x < playerTransform.position.x) {
                deltaPos.x = deltaX - boundX;
            } else {
                deltaPos.x = deltaX + boundX;
            }
        }

        if (deltaY > boundY || deltaY < -boundY) {
            if (transform.position.y < playerTransform.position.y) {
                deltaPos.y = deltaY - boundY;
            } else {
                deltaPos.y = deltaY + boundY;
            }
        }
        deltaPos.z = 0;
        transform.position += deltaPos;
    }
}
