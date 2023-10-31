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
        slider.value = PlayerPrefs.GetFloat("volume", 1f);
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        volume = Mathf.Log10(slider.value) * 20;
        if (volume == 0.0f)
        {
            volume = -80.0f;
        }

        Events<VolumeChangeEvent>.Instance.Trigger(slider.value);
        PlayerPrefs.SetFloat("volume", slider.value);
    }

}



