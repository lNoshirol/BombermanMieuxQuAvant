using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStateMachine : MonoBehaviour
{
    public IBotState currentState;

    public SearchBombState searchBombState = new();
    public RunAwayState runAwayState = new();
    public AttackState attackState = new();

    public void ChangeState(IBotState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IBotState
{
    public void OnEnter(BotStateMachine botStateMachine);

    public void OnExit(BotStateMachine botStateMachine);
}