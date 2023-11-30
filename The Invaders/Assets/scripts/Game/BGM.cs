using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update

    void OnVolumeChange(float v) {
        if (audioSource != null)
        {
            audioSource.volume = v;
        }
    }

    void OnEnable()
    {
        Events<VolumeChangeEvent>.Instance.Register(OnVolumeChange);
    }
    void OnDisable() {
        Events<VolumeChangeEvent>.Instance.Unregister(OnVolumeChange);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("volume", 0.2f); // 20% of volume.
        audioSource.Play();
        audioSource.loop = true;
    }
}
