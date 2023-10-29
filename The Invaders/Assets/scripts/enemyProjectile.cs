using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 4.5f;
    // Update is called once per frame
    private Vector3 target;

    [SerializeField]
    private Rigidbody2D body;

    [SerializeField]
    private CircleCollider2D circel;

    [SerializeField]
    private float halfLife;
    private float startTime;

    private Vector3 direction;
    void Start()
    {
        startTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        direction = (target - transform.position).normalized * speed;
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
        if(startTime + halfLife <= Time.time)
            Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Player"))
        {
            Events<TakeDamageEvent>.Instance.Trigger?.Invoke(5f);
            Destroy(gameObject);
        }
    }
}
