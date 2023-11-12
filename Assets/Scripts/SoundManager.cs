using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string _name;
    public AudioClip _clip;

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private Sound[] _musicSounds, sfxSounds;
    [SerializeField] private AudioSource _musicSource, _sfxSource;
    public AudioSource MusicSource => _musicSource;
    public AudioSource SfxSource => _sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        _musicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("MusicMute"));
        _sfxSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("SoundMute"));
    }


    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(_musicSounds, x => x._name == name);

        if(sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _musicSource.clip = sound._clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x._name == name);

        if (sound == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            _sfxSource.clip = sound._clip;
            _sfxSource.loop = false;
            _sfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
        PlayerPrefs.SetInt("MusicMute", Convert.ToInt32(_musicSource.mute));
        PlayerPrefs.Save();
    }

    public void ToggleSFX()
    {
        _sfxSource.mute = !_sfxSource.mute;
        PlayerPrefs.SetInt("SoundMute", Convert.ToInt32(_sfxSource.mute));
        PlayerPrefs.Save();
    }
}

