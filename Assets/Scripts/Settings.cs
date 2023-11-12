using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _settingButton, _soundButton, _musicButton;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Sprite _buttonOn, _buttonOff;
    [SerializeField] private SoundSettings _soundSettings;

    private void Start()
    {
        ChangeSoundButtonSprite();
        ChangeMusicButtonSprite();
    }

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

    public void ChangeSoundButtonSprite()
    {
        if(!_soundSettings.SoundMute)
        {
            _soundButton.GetComponent<Image>().sprite = _buttonOn;
        }
        else
        {
            _soundButton.GetComponent<Image>().sprite = _buttonOff;
        }
    }
    public void ChangeMusicButtonSprite()
    {
        if (!_soundSettings.MusicMute)
        {
            _musicButton.GetComponent<Image>().sprite = _buttonOn;
        }
        else
        {
            _musicButton.GetComponent<Image>().sprite = _buttonOff;
        }
    }

}
