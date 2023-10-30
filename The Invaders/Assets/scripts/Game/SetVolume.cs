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
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    public void ValueChangeCheck()
    {
        volume = slider.value;
        Events<VolumeChangeEvent>.Instance.Trigger(slider.value);
    }

}



