using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class CameraFollow : MonoBehaviour, ISaveable
{
    private Transform playerTransform;
    [SerializeField] private float boundX = 0.3f, boundY = 0.15f;
    private Vector3 deltaPos;
    private float deltaX, deltaY;

    public float CameraSpeed = 5f;
    public static GameObject cameraHolder;

    private static bool seenIntro = false;

    public bool alwaysWatchIntro = false;

    public Camera mainCam;
    public Camera dollyCam;

    public float timeDolly = 10;
    private playerMovement pm;

    IEnumerator Start()
    {
        pm = playerTransform.GetComponent<playerMovement>();
        SaveManager.Instance.LoadData(this);
        if (dollyCam)
        {
            if ((alwaysWatchIntro || !seenIntro))
            {
                StartIntro();
                yield return new WaitForSeconds(timeDolly);
                StopIntro();
            }

            dollyCam.gameObject.SetActive(false);
        }
    }

    void StartIntro()
    {
        mainCam.enabled = false;
        dollyCam.enabled = true;
        pm.LockMovement();
    }

    void StopIntro()
    {
        dollyCam.enabled = false;
        mainCam.enabled = true;
        pm.UnlockMovement();
        seenIntro = true;
        SaveManager.Instance.SaveData(this);
        Events<StartDialogue>.Instance.Trigger?.Invoke("village_start");
    }

    void Awake()
    {
        cameraHolder = this.gameObject;
        playerTransform = Player.getPlayerObject().transform;
    }

    void Update()
    {
        if (!seenIntro && Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(Start());
            StopIntro();
        }
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
    
    public void PopulateSaveData(SaveData save)
    {
        save.seenIntroCam = seenIntro;
    }

    public void LoadFromSaveData(SaveData save)
    {
        seenIntro = save.seenIntroCam;
        Debug.Log("intro seen: " + save.seenIntroCam);
    }
}
