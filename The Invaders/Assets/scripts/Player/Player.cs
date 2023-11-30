using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour, ISaveable
{
    private static bool customSpawn = false;
    public static Vector3 spawnPos { private set; get; }

    private GameObject player;
    private GameObject cameraHolder;
    private HealthBar healthbar;
    private StaminaBar staminabar;
    public Quest currentQuest;
    public InventoryManager inventoryManager;
    // Game data, will fix persistence soon.
    public int progress = 0;


    private void Awake()
    {
        player = getPlayerObject();
    }

    void Start()
    {        
        SaveManager.Instance.LoadData(this);
        
        if(customSpawn)
        {
            player.transform.position = spawnPos;
            CameraFollow.cameraHolder.transform.position = new Vector3(spawnPos.x, spawnPos.y, CameraFollow.cameraHolder.transform.position.z);
        }
        customSpawn = false;
        spawnPos = player.transform.position;
        // DontDestroyOnLoad(gameObject);
        inventoryManager = GameObject.Find("Canvas").GetComponentInChildren<InventoryManager>();
        if(inventoryManager.hasSword == true)
        {
            Debug.Log("we have sword!");
            player.GetComponent<Animator>().SetTrigger("PickedUpSword");
        }
    }

    public static void setCustomSpawn(Vector3 pos)
    {
        if (!customSpawn)
        {
            customSpawn = true;
            spawnPos = pos;
        }
    }

    public static GameObject getPlayerObject() {
        return GameObject.FindWithTag(TagManager.PLAYER_TAG);
    }

    public void PopulateSaveData(SaveData save)
    {
        save.playerData.playerProgress = progress;
        save.playerData.lastScene = SceneManager.GetActiveScene().name;
        save.playerData.spawnPos = player.transform.position;
    }

    public void LoadFromSaveData(SaveData save)
    {
        if (save.seenIntroCam)
        {
            progress = save.playerData.playerProgress;

            if (SceneManager.GetActiveScene().name.Equals(save.playerData.lastScene))
            {
                Debug.Log("custom spawn set!");
                setCustomSpawn(save.playerData.spawnPos);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SaveManager.Instance.ForceSave();
        }
    }
}