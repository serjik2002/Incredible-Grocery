using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEntryPoint : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayMusic("BackgroundMusic");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
