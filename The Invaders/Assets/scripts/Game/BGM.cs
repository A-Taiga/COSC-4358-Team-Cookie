using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        Events<VolumeChangeEvent>.Instance.Register(v => {
            if (audioSource != null)
            {
                audioSource.volume = v * 0.2f;
            }
        });
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.2f); // 20% of volume.
        audioSource.Play();
        audioSource.loop = true;
    }
}
