using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public delegate void TakeDamageEvent(float damage);

public class HealthBar : MonoBehaviour, ISaveable
{
    public Slider slider;
    private float health;

    private bool isDead;

    void OnTakeDamage(float damage) {
        if (this.health < damage)
        {
            // Dead...
            isDead = true;
            Time.timeScale = 0f;
            PauseManager.isPaused = true;
            GUIController.Instance.SetDeathScreen(true);
        }
        else
        {
            SetHealth(health - damage);
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    void OnEnable()
    {
        SaveManager.Instance.LoadData(this);
        Events<TakeDamageEvent>.Instance.Register(OnTakeDamage);
        if (health <= 0)
        {
            SetHealth(100);
        }
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
        
        if (health >= slider.maxValue)
        {
            health = slider.maxValue;
            slider.value = health;
        }
    }
    public float GetHealth()
    {
        return health;
    }

    public void AddHealth(float value)
    {
        health += value;
        slider.value = health;
        if (health >= slider.maxValue)
        {
            health = slider.maxValue;
            slider.value = health;
        }
    }
    
    public void PopulateSaveData(SaveData save)
    {
        if (!isDead)
        {
            save.playerData.playerHealth = GetHealth();
        }
    }

    public void LoadFromSaveData(SaveData save)
    {
        if (save.seenIntroCam)
        {
            SetHealth(save.playerData.playerHealth);
        }
    }

}



