using System.Collections;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField]
    private float boundX = 0.3f, boundY = 0.15f;
    private Vector3 deltaPos;
    private float deltaX, deltaY;

    public float CameraSpeed = 5f;


    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void LateUpdate() {
        Follow();
    }
    private void Follow() { 
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
        Vector3 targetPos = playerTransform.position + deltaPos;
        targetPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPos, CameraSpeed * Time.deltaTime);
    }
}
