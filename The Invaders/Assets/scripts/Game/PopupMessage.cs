using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{
    public GameObject worldCanvas;
    public Text popupText;
    private string message;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        message = string.Empty;
        time = 0f;
    }

    public void ShowPopup(string msg, float timeToShow)
    {
        message = msg;
        time = timeToShow;
        StartCoroutine(PopupHrtBeat());
    }

    IEnumerator PopupHrtBeat()
    {
        worldCanvas.SetActive(true);
        popupText.text = message;
        yield return new WaitForSeconds(time);
        worldCanvas.SetActive(false);
    }
}
