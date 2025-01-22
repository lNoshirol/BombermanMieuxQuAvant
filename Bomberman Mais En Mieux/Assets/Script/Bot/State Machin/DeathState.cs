using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DeathState : IBotState
{
    private BotStateMachine _stateMachine;
    private BotBRAIN _brain;

    public void OnEnter(BotStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _brain = _stateMachine.botBrain;

        _brain.GetComponent<NavMeshAgent>().speed = 0;
    }

    public void StateUpdate()
    {

        //Debug.Log($"{_stateMachine.gameObject.name}, est mort.");
    }

    public void OnExit(BotStateMachine stateMachine)
    {
        Debug.Log("What do you think you're doing ?... \n nobody can escape the DeathState");
        stateMachine.currentState = stateMachine.deathState;
    }
}
