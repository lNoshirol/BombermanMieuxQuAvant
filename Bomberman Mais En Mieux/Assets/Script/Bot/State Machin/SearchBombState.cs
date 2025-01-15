using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBombState : IBotState
{
    private BotStateMachine _stateMachine;
    private BombPoolObject _pool;
    private BotBRAIN _brain;

    public void OnEnter(BotStateMachine botStateMachine)
    {
        _pool = BombPoolObject.instance;

        _stateMachine = botStateMachine;
        _stateMachine.currentState = this;
        _brain = _stateMachine.botBrain;
    }

    public void StateUpdate()
    {
        if (this == _stateMachine.currentState && _brain.player != null)
        {
            if (_brain.GetBombNumber() == 0 || (_brain.GetBombNumber() < _brain.player.GetComponent<PlayerPickDrop>().GetBombNumber()))
            {
                _brain.BOTFindNearestBombe();
            }
            else if (_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _brain.player.GetComponent<PlayerPickDrop>().GetBombNumber())
            {
                Debug.Log("je peux attaquer");
            }
        }
    }

    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}