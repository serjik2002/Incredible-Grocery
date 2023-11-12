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

    private void Awake()
    {
        ChangeSoundButtonSprite();
        ChangeMusicButtonSprite();
    }

    private void Start()
    {
        _soundButton.onClick.AddListener(SoundManager.Instance.ToggleSFX);
        _soundButton.onClick.AddListener(ChangeSoundButtonSprite);
        _soundButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX("ButtonClick");
        });


        _musicButton.onClick.AddListener(SoundManager.Instance.ToggleMusic);
        _musicButton.onClick.AddListener(ChangeMusicButtonSprite);
        _musicButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX("ButtonClick");
        });

        
        
        
    }

    public void OpenSettings()
    {
        Debug.Log("OpenSettings");
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
