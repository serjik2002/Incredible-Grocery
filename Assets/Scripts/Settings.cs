using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _saveButton;
    [SerializeField] private GameObject _settingsPanel;



    public void OpenSettings()
    {
        _settingsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void SaveSettings()
    {
        _settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

}