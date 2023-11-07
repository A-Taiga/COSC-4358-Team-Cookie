using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider slider;
    private Camera camera;
    private Transform target;
    [SerializeField] private Vector3 offset;
    //[SerializeField] private Camera mainCamera;
    //[SerializeField] private Transform target;

    void Awake()
    {
        slider = GetComponent<Slider>();
        camera = Camera.main;
        target = transform.parent.transform.parent.transform;
        SetMaxHealthBar(100f);
        UpdateHealthBar(100f);
    }

    public void UpdateHealthBar(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealthBar(float max)
    {
        slider.maxValue = max;
    }

    void LateUpdate()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
