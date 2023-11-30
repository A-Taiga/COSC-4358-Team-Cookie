using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestVillager : MonoBehaviour
{
    public Sprite healedSprite;
    public static bool injured = true;
    public Dialogue savedDialog;

    IEnumerator Start()
    {
        Events<EndDialogue>.Instance.Register(EndCheck);
        while (true)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            if (enemies.Length <= 0)
            {
                injured = false;
                SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
                sr.sprite = healedSprite;
                break;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    void EndCheck(string name)
    {
        if (name.Equals("forest_injured_npc"))
        {
            Player player = Player.getPlayerObject().GetComponent<Player>();

            player.progress = 2;
            player.gameObject
                .GetComponentInChildren<PopupMessage>()
                .ShowPopup("Maybe I should check out Sunset Bay...", 5f);
            SaveManager.Instance.SaveData(player);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!injured)
        {
            if (savedDialog.dialogSeen)
            {
                this.gameObject.GetComponentInChildren<PopupMessage>().ShowPopup("Thank you once again for saving me. They keep coming back...", 3f);
            }
            else
            {
                Events<StartDialogue>.Instance.Trigger?.Invoke("forest_injured_npc");
            }
        }
        else
        {
            this.gameObject.GetComponentInChildren<PopupMessage>().ShowPopup("Help me... I'm scared..", 3f);
        }
    }
}
