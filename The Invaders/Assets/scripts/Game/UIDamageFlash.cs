using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageFlash : MonoBehaviour
{
    private Color startColor;
    public Image heartSprite;

    bool active;

    void Awake()
    {
        active = false;
        heartSprite = GetComponent<Image>();
    }

    public void OnTakeDamage(float amount)
    {
        Debug.Log("damage !");
        if(heartSprite && !active) {
            startColor = Color.red;
            StartCoroutine(flashHeart());
        }
    }
    public void OnEnable()
    {
        heartSprite.color = Color.red;
        Events<TakeDamageEvent>.Instance.Register(OnTakeDamage);
    }
    public void OnDisable()
    {
        Events<TakeDamageEvent>.Instance.Unregister(OnTakeDamage);
    }

    IEnumerator flashHeart()
    {
        active = true;
        for (int i = 0; i <= 3; i++)
        {
            heartSprite.color = Color.white;
            yield return new WaitForSeconds(0.08f);
            heartSprite.color = startColor;
            yield return new WaitForSeconds(0.08f);
        }
        active = false;
    }
    
}
