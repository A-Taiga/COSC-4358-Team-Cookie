using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void TakeDamageEvent(float damage);

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    private float health;

    void OnTakeDamage(float damage) {
        if (this.health < damage)
        {
            SetHealth(100 - damage);
        }
        else
        {
            SetHealth(health - damage);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void OnEnable()
    {
        Events<TakeDamageEvent>.Instance.Register(OnTakeDamage);
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void OnDisable()
    {
        Events<TakeDamageEvent>.Instance.Unregister(OnTakeDamage);
    }
    void Awake()
    {
        SetMaxHealth(100f);
        SetHealth(100f);
    }
    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
    }

    public void SetHealth(float value)
    {
        health = value;
        slider.value = health;
    }
    public float GetHealth()
    {
        return health;
    }

    public void AddHealth(float value)
    {
        health += value;
        slider.value = health;
    }

}



