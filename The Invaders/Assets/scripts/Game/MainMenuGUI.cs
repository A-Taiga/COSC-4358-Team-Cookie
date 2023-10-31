using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGUI : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settings;

    public void OpenSettings()
    {
        settings.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void SettingsDone()
    {
        mainPanel.SetActive(true);
        settings.SetActive(false);
    }
}
