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
    public DeathState deathState = new();

    public BotBRAIN botBrain;

    private void Awake()
    {
        botBrain = GetComponent<BotBRAIN>();
        GetComponent<PlayerHealth>().OnDamageTook += DamageTaken;
    }

    private void Start()
    {
        ChangeState(searchBombState);
    }

    public void ChangeState(IBotState newState)
    {
        if (currentState == deathState) return;

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

    public void DamageTaken(int pv)
    {
        if (pv == 0)
        {
            ChangeState(deathState);
        }
    }
}



public interface IBotState
{
    public void OnEnter(BotStateMachine botStateMachine);

    public void StateUpdate();

    public void OnExit(BotStateMachine botStateMachine);
}