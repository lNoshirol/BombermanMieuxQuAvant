using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IBotState
{
    private BotStateMachine _stateMachine;

    public void OnEnter(BotStateMachine botStateMachine)
    {

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
