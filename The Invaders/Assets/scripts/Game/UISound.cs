using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    float volume;
    public AudioSource clickSound;
    public AudioSource hoverSound;


    void OnVolumeChange(float v) {
        volume = v;
    }
    void Start()
    {
        volume = PlayerPrefs.GetFloat("volume", 0.8f);
        Events<VolumeChangeEvent>.Instance.Register(OnVolumeChange);
    }
    void OnDisable() {
        Events<VolumeChangeEvent>.Instance.Register(OnVolumeChange);
    }

    public void OnPointerEnter(PointerEventData ped)
    {
        playHoverSound();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        playClickSound();
    }

    public void playHoverSound()
    {
        if (hoverSound)
        {
            hoverSound.volume = volume;
            hoverSound.Play();
        }
    }
    public void playClickSound()
    {
        if (clickSound)
        {
            clickSound.volume = volume;
            clickSound.Play();
        }
    }

}