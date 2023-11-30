using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sprite;
    public Sprite open;

    public Animator animator;

    public GameObject myPrefab;
    [SerializeField]
    public Item item;

    private bool done = false;

    private bool opened = false;

    public string keyName;

    private Player player;

    public AudioClip rattle;
    public AudioClip openSound;

    private AudioSource srcPlayer;
    
    void Start()
    {
        srcPlayer = InventoryManager.Instance.transform.root.gameObject.GetComponent<AudioSource>();
        myPrefab.GetComponent<WorldItem>().item = item;
        player = Player.getPlayerObject().GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        // gameObject.GetComponent<Animator>().

        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "done" && !done)
        {
            Instantiate(myPrefab, new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z), Quaternion.identity);
            done = true;
        }

        
    }

    void OnCollisionEnter2D()
    {
        
        if(opened == true)
            return;
        
        if (keyName == "wetlands" && !player.hasWetlandsKey)
        {
            // rattle lock.
            if (srcPlayer && rattle)
            {
                srcPlayer.clip = rattle;
                srcPlayer.Play();
            }
            player.GetComponentInChildren<PopupMessage>().ShowPopup("Looks like the chest is locked.", 5f);
            return;
        } 
        else if (keyName == "volcano")
        {
            if (player.hasShield)
            {
                if (srcPlayer && rattle)
                {
                    srcPlayer.clip = rattle;
                    srcPlayer.Play();
                }
                return;
            }
        }

        Debug.Log("Collision");
        if (srcPlayer && openSound)
        {
            srcPlayer.clip = openSound;
            srcPlayer.Play();
        }
        // sprite.sprite = open;
        // animator.SetTrigger("open");
        gameObject.GetComponent<Animator>().SetBool("open", true);
        // sprite.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
        // Instantiate(myPrefab, new Vector3(transform.position.x,transform.position.y-0.1f,transform.position.z), Quaternion.identity);
        opened = true;
    }
}
