using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;
    public Sprite open;

    public GameObject myPrefab;
    [SerializeField]
    public Item item;

    private bool opened = false;
    void Start()
    {
        myPrefab.GetComponent<WorldItem>().item = item;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D()
    {
        if(opened == true)
            return;
        Debug.Log("Collision");
        sprite.sprite = open;
        sprite.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        Instantiate(myPrefab, new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z), Quaternion.identity);
        opened = true;
    }
}
