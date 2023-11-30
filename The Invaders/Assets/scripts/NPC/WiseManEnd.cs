using UnityEngine;

public class WiseManEnd : MonoBehaviour
{
    private Player player;

    public Item[] drops;
    public GameObject worldItem;

    public float talkTime;
    
    void Start()
    {
        player = Player.getPlayerObject().GetComponent<Player>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time - talkTime < 2.5)
        {
            return;
        }
        if (player.diamonds >= 3 || player.hasWetlandsKey)
        {
            Events<StartDialogue>.Instance.Trigger?.Invoke("wizard_npc");
        }
        else
        {
            Events<StartDialogue>.Instance.Trigger?.Invoke("diamond_quest");
        }
    }
    public void EndCheck(string npc)
    {
        Debug.Log(npc);
        if (npc.Equals("diamond_quest"))
        {
            player.GetComponentInChildren<PopupMessage>().ShowPopup("I should look around at a Beach?", 5f);
            if (player.progress < 3)
            {
                player.progress = 3;
                SaveManager.Instance.SaveData(player);
            }
        }
        else if (npc.Equals("wizard_npc"))
        {
            if (!player.hasWetlandsKey)
            {
                DropItems();
            }
            player.hasWetlandsKey = true;
            player.GetComponentInChildren<PopupMessage>().ShowPopup("What can I do with this key?", 5f);
            if (player.progress < 3)
            {
                player.progress = 3;
            }
            SaveManager.Instance.SaveData(player);
        }
        else
        {
            return;
        }

        talkTime = Time.time;
    }

    void OnEnable()
    {
        Events<EndDialogue>.Instance.Register(EndCheck);
    }

    private void OnDisable()
    {
        Events<EndDialogue>.Instance.Unregister(EndCheck);
    }
    
    void DropItems()
    {
        if (!worldItem)
            return;

        foreach (var item in drops)
        {
            try
            {
                Vector2 offsets = Random.insideUnitCircle.normalized * 0.16f;
                worldItem.GetComponent<WorldItem>().item = item;
                Instantiate(worldItem, (Vector2)gameObject.transform.position + offsets, Quaternion.identity);
            } catch {}
        }
    }
}