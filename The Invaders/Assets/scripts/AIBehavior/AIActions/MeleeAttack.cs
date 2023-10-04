using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AIAction
{
    // https://gist.github.com/st4rdog/82a4d99c4f6eb59efa162a05ec62163b
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;


    private bool isShaking;

    void Awake()
    {
        isShaking = false;
    }

    public override float Desire(RaycastHit2D[] rays)
    {
        this.desire = 0;
        if (this.collidingPlayer && !isShaking) {
            this.desire = 1;
        }
        return this.desire;
    }

    public override void Execute()
    {
        isShaking = true;
        _startPos = transform.position;
        StopAllCoroutines();
        StartCoroutine(Shake());
        //Debug.Log("shakey shakey");
    }

    private IEnumerator Shake()
    {
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (UnityEngine.Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                isShaking = false;
                yield return null;
            }
        }

        transform.position = _startPos;
    }
}
