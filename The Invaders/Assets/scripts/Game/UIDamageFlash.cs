using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDamageFlash : MonoBehaviour
{
    private Color startColor;
    public SpriteRenderer heartSprite;
    void OnTakeDamage(float amount)
    {
        if(heartSprite) {
            startColor = heartSprite.color;
            StartCoroutine(flashHeart());
        }
    }
    public void onEnable()
    {
        Events<TakeDamageEvent>.Instance.Register(OnTakeDamage);
    }
    public void onDisable()
    {
        Events<TakeDamageEvent>.Instance.Unregister(OnTakeDamage);
    }

    IEnumerator flashHeart()
    {
        for (int i = 0; i <= 2; i++)
        {
            heartSprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            heartSprite.color = startColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
