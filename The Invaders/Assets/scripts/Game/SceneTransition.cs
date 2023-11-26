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
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneIdx);
        transitionAnim.SetTrigger("Start");
    }
    IEnumerator LoadLevel(string name)
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(name);
        transitionAnim.SetTrigger("Start");
    }
}
