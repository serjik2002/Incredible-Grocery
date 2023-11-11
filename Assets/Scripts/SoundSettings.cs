using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoundSettings")]

public class SoundSettings : ScriptableObject
{
    [SerializeField] private bool _musicMute;
    [SerializeField] private bool _soundMute;

    public bool MusicMute => _musicMute;
    public bool SoundMute => _soundMute;

    public void SetMusicMute(bool value)
    {
        _musicMute = value;
    }

    public void SetSoundMute(bool value)
    {
        _soundMute = value;
    }
}
