using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class IntroScene : MonoBehaviour
{

    public Slider skipBar;
    public TextMeshProUGUI skipText;

    float lastSkip;
    float skipHealth = 2f;
    private VideoPlayer[] videos;
    int index;

  

    void Awake()
    {
        lastSkip = 0;
        index = 0;
        videos = GetComponentsInChildren<VideoPlayer>();

        if(videos != null)
        {
            foreach (var vp in videos)
            {
                vp.loopPointReached += EndReached;
            }
            videos[index].Play();
        }
        if(skipBar != null)
        {
            skipBar.maxValue = skipHealth;
            skipBar.value = 0f;
        }
        if(skipText != null)
        {
            skipText.text = "";
        }
    }

    void PlayNext()
    {
        index++;
        if (index < videos.Length)
        {
            videos[index].Play();
        }
        else
        {
            SceneLoadingManager.LoadScene("enemyTestScene");
        }
    }

    void EndReached(VideoPlayer vp)
    {
        Debug.Log($"Video {index} ended.");
        PlayNext();
    }

    void Update()
    {
        if (skipBar)
        { 
            if (Input.GetKey(KeyCode.Return))
            {
                if (skipBar.value < skipHealth)
                {
                    if (Time.time - lastSkip > 2f)
                    {
                        skipBar.value += Time.deltaTime;
                        skipText.text = "SKIPPING...";
                    }
                }
                else
                {
                    lastSkip = Time.time;
                    skipBar.value = 0f;
                    videos[index].Stop();
                    PlayNext();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Return))
            {
                skipBar.value = 0f;
                skipText.text = "";
            }
        }
    }

}
