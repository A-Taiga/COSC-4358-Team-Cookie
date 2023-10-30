using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void VolumeChangeEvent(float volume);

public class SetVolume : MonoBehaviour
{
    public Slider slider;
    private float volume;

    void Start()
    {
        volume = PlayerPrefs.GetFloat("volume", 1f);
        slider.value = volume;
        Events<VolumeChangeEvent>.Instance.Trigger(volume);
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    public void ValueChangeCheck()
    {
        volume = slider.value;
        Events<VolumeChangeEvent>.Instance.Trigger(slider.value);
        PlayerPrefs.SetFloat("volume", volume);
    }

}



