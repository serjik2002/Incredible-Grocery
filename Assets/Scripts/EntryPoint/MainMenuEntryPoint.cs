using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEntryPoint : MonoBehaviour
{
    private SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        _soundManager.PlayMusic("BackgroundMusic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
