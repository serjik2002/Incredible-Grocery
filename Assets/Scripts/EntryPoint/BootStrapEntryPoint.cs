using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootStrapEntryPoint : MonoBehaviour
{
    private IEnumerator Start()
    {
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}
