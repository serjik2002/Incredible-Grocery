using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Button _settingButton, _soundButton, _musicButton;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private Sprite _buttonOn, _buttonOff;

    private void Awake()
    {
        
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
        _settingsPanel.SetActive(true);
        ChangeSoundButtonSprite();
        ChangeMusicButtonSprite();
        Time.timeScale = 0;
        
    }

    public void SaveSettings()
    {
        _settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeSoundButtonSprite()
    {
        if(!SoundManager.Instance.SfxSource.mute)
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
        if (!SoundManager.Instance.MusicSource.mute)
        {
            _musicButton.GetComponent<Image>().sprite = _buttonOn;
        }
        else
        {
            _musicButton.GetComponent<Image>().sprite = _buttonOff;
        }
    }

}
