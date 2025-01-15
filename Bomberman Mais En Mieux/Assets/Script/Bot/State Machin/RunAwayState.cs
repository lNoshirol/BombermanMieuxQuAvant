using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAwayState : IBotState
{
    private BotStateMachine _stateMachine;

    private Vector3 _playerPosition;

    public void OnEnter(BotStateMachine botStateMachine)
    {
        _stateMachine = botStateMachine;
        _stateMachine.currentState = this;
    }

    public void StateUpdate()
    {
        if (this == _stateMachine.currentState)
        {

        }
    }

    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}
