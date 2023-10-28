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

    private Vector3 direction;
    void Start()
    {
        // body = GetComponent<Rigidbody2D>();
        // circel = GetComponent<CircleCollider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        direction = (target - transform.position).normalized * speed;

    }
    
    void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
