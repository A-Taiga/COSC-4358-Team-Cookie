using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    public VideoPlayer[] videos;
    int index;

    void Awake()
    {
        index = 0;
        videos = GetComponentsInChildren<VideoPlayer>();

       if(videos != null)
        {
            videos[index].loopPointReached += EndReached;
            videos[index].Play();
            index++;
        }
    }

    void EndReached(VideoPlayer vp)
    {
        Debug.Log($"Video {index} ended.");
        if (index < videos.Length)
        {
            videos[index].loopPointReached += EndReached;
            videos[index].Play();
            index++;
        } 
        else
        {
            SceneManager.LoadScene("enemyTestScene");
        }
    }

}
