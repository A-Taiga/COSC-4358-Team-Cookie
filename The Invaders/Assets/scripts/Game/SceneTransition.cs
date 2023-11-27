using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator transitionAnim;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ChangeLevel(int scene)
    {
        StartCoroutine(LoadLevel(scene));
    }
    public void ChangeLevel(string name)
    {
        StartCoroutine(LoadLevel(name));
    }

    IEnumerator LoadLevel(int sceneIdx)
    {
        var pm = Player.getPlayerObject().GetComponent<playerMovement>();
        pm.LockMovement();
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneIdx);
        transitionAnim.SetTrigger("Start");
        pm.UnlockMovement();
    }
    IEnumerator LoadLevel(string name)
    {
        var pm = Player.getPlayerObject().GetComponent<playerMovement>();
        pm.LockMovement();
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(name);
        transitionAnim.SetTrigger("Start");
        pm.UnlockMovement();
    }
}
