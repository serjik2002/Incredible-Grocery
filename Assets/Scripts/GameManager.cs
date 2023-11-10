using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    private GameState _currentState;

    public UnityEvent OnStartGame;
    public UnityEvent OnEndGame;
    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }


}
