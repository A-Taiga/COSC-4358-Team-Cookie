using System;
using System.Collections;
using UnityEngine;
using TMPro;


public delegate void StartDialogue(string name);
public delegate void EndDialogue(string name);

public class Dialogue : MonoBehaviour, ISaveable
{
    public string dialogIdentifier;

    public string title;
    
    public TextMeshProUGUI titleComp;
    public TextMeshProUGUI textComponent;
    
    [TextArea]
    public string[] lines;
    public float textSpeed;

    public bool started { private set; get; } = false;
    private int index;

    private Animator playerAnimator;

    public bool dialogRepeatable = true;
    public bool dialogSeen = false;
    
    public void DialogueCheck(string startName)
    {
        if (dialogSeen && !dialogRepeatable)
        {
            return;
        }

        if (!started && startName.Equals(dialogIdentifier))
        {
            StartDialogue();
        }
    }
    
    void Start()
    {
        SaveManager.Instance.LoadData(this);
        textComponent.text = string.Empty;

        if (string.IsNullOrEmpty(title))
        {
            titleComp.text = string.Empty;
        }
        else
        {
            titleComp.text = title;
        }
        
        Events<StartDialogue>.Instance.Register(DialogueCheck);
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        Events<StartDialogue>.Instance.Unregister(DialogueCheck);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        started = true;
        gameObject.SetActive(true);
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
        PauseManager.isPaused = true;
        playerAnimator = Player.getPlayerObject().GetComponent<Animator>();
        playerAnimator.speed = 0f;
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            playerAnimator.speed = 1f;
            started = false;
            PauseManager.isPaused = false;
            Events<EndDialogue>.Instance.Trigger?.Invoke(dialogIdentifier);
            gameObject.SetActive(false);
            dialogSeen = true;
            SaveManager.Instance.SaveData(this);
        }
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        } 
    }
    
    public void PopulateSaveData(SaveData save)
    {
        if (dialogIdentifier.Trim().Length > 0)
        {
            var dialog = new SaveData.DialogueData();
            dialog.dialogName = dialogIdentifier;
            dialog.dialogSeen = dialogSeen;
            
            int idx = save.m_DialogueData.RemoveAll(d => { return d.dialogName == dialogIdentifier; });
            save.m_DialogueData.Add(dialog);   
        }
    }

    public void LoadFromSaveData(SaveData save)
    {
        if (dialogIdentifier.Trim().Length > 0)
        {
            foreach (var dialog in save.m_DialogueData)
            {
                if (dialog.dialogName.Equals(dialogIdentifier))
                {
                    dialogSeen = dialog.dialogSeen;
                    break;
                }
            }
        }
    }
}
