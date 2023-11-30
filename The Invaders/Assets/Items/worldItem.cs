using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour, ISaveable
{


    [SerializeField]
    public Item item;
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public InventoryManager inventoryManager;

    public bool respawn = true;
    private int hash;
    private bool pickedUp = false;

    private float spawnTime;

    public AudioClip pickupSound;
    private AudioSource src;
    
    void Awake()
    {
    }
    
    void Start()
    {
        src = InventoryManager.Instance.transform.root.gameObject.GetComponent<AudioSource>();
        spawnTime = Time.time;
        string temp = "item_" + gameObject.name + item.itemName + SceneName.sceneName;
        hash = GetFNV1aHashCode(temp);
        SaveManager.Instance.LoadData(this);
        
        if (!respawn && pickedUp)
        {
            Destroy(gameObject);
        }
        spriteRenderer.sprite = item.image;
        inventoryManager = InventoryManager.Instance;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - spawnTime < 0.5)
        {
            return;
        }
        
        if(collision.gameObject.CompareTag("Player"))
        {
            if(item.name == "sword")
            {
                Player player = Player.getPlayerObject().GetComponent<Player>();
                if (player.progress < 4)
                {
                    player.progress = 4;
                    player.GetComponentInChildren<PopupMessage>().ShowPopup("I can now enter the Volcano Islands!", 5f);
                }

                collision.gameObject.GetComponent<Animator>().SetTrigger("PickedUpSword");
                inventoryManager.hasSword = true;
                SaveManager.Instance.SaveData(inventoryManager);
                Destroy(gameObject);
                return;
            } 
            else if (item.name == "shield")
            {
                Player player = Player.getPlayerObject().GetComponent<Player>();
                if (player.progress < 5)
                {
                    player.progress = 5;
                    player.GetComponentInChildren<PopupMessage>().ShowPopup("This looks like it might be useful in the final battle.", 5f);
                }

                player.hasShield = true;
                SaveManager.Instance.SaveData(player);
            } 
            else if (item.name == "Diamond")
            {
                Player player = Player.getPlayerObject().GetComponent<Player>();
                player.diamonds += 1;
                if (player.diamonds == 3)
                {
                    player.GetComponentInChildren<PopupMessage>().ShowPopup("That should be all of them.", 5f);
                }
                SaveManager.Instance.SaveData(player);
            }

            if (pickupSound)
            {
                Debug.Log("play");
                src.clip = pickupSound;
                src.Play();
            }
            
            inventoryManager.AddItem(item);
            pickedUp = true;
            SaveManager.Instance.SaveData(this);
            Destroy(gameObject);
        }
    }
    // https://stackoverflow.com/questions/7534169/string-gethashcode-returns-different-values-in-debug-vs-release-how-do-i-avoi
    /// <summary>
    /// Default implementation of string.GetHashCode is not consistent on different platforms (x32/x64 which is our case) and frameworks. 
    /// FNV-1a - (Fowler/Noll/Vo) is a fast, consistent, non-cryptographic hash algorithm with good dispersion. (see http://isthe.com/chongo/tech/comp/fnv/#FNV-1a)
    /// </summary>
    private static int GetFNV1aHashCode(string str)
    {
        if (str == null)
            return 0;
        var length = str.Length;
        // original FNV-1a has 32 bit offset_basis = 2166136261 but length gives a bit better dispersion (2%) for our case where all the strings are equal length, for example: "3EC0FFFF01ECD9C4001B01E2A707"
        int hash = length;
        for (int i = 0; i != length; ++i)
            hash = (hash ^ str[i]) * 16777619;
        return hash;
    }
    
    public void PopulateSaveData(SaveData save)
    {
        if (respawn)
            return;
        
        int idx = save.m_WorldItems.FindIndex(w => { return w.wiHash == hash; });
        if (idx >= 0)
        {
            save.m_WorldItems.RemoveAt(idx);
        }

        SaveData.WorldItem wi;
        wi.wiHash = hash;
        wi.wiPickedUp = pickedUp;
        
        save.m_WorldItems.Add(wi);
    }

    public void LoadFromSaveData(SaveData save)
    {
        if (respawn)
            return;
        var wi = save.m_WorldItems.Find(w => { return w.wiHash == hash; });
        
        pickedUp = wi.wiPickedUp;
        
        save.m_WorldItems.Remove(wi);
    }
}
