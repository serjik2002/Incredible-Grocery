using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState
{
    protected GameManager _gameManager;
    
    public GameState(GameManager gameManager)
    {

    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

public class MainMemuState : GameState
{
    public MainMemuState(GameManager gameManager) : base(gameManager) { }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

}

public class PlayState : GameState
{
    public PlayState(GameManager gameManager) : base(gameManager) { }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

}
