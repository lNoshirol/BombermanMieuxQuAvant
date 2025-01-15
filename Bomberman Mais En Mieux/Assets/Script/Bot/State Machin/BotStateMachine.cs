using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotStateMachine : MonoBehaviour
{
    public IBotState currentState;

    public SearchBombState searchBombState = new();
    public RunAwayState runAwayState = new();
    public AttackState attackState = new();

    public BotBRAIN botBrain;

    private void Awake()
    {
        botBrain = GetComponent<BotBRAIN>();
    }

    private void Start()
    {
        ChangeState(searchBombState);
    }

    public void ChangeState(IBotState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        currentState.StateUpdate();
    }
}



public interface IBotState
{
    public void OnEnter(BotStateMachine botStateMachine);

    public void StateUpdate();

    public void OnExit(BotStateMachine botStateMachine);
}