using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void OnSceneChange(string sceneName);

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator transitionAnim;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root);
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
        yield return new WaitForSeconds(1f);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone){
            yield return null;
        }
        Events<OnSceneChange>.Instance.Trigger?.Invoke(SceneManager.GetActiveScene().name);
        transitionAnim.SetTrigger("Start");
        pm.UnlockMovement();
    }
    IEnumerator LoadLevel(string name)
    {
        var pm = Player.getPlayerObject().GetComponent<playerMovement>();
        pm.LockMovement();
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone){
            yield return null;
        }
        Events<OnSceneChange>.Instance.Trigger?.Invoke(name);
        transitionAnim.SetTrigger("Start");
        pm.UnlockMovement();
    }
}
