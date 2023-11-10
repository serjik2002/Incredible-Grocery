using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnStartGame;
    public UnityEvent OnEndGame;
    public UnityEvent OnLevelStart;
    public UnityEvent OnLevelEnd;
}
