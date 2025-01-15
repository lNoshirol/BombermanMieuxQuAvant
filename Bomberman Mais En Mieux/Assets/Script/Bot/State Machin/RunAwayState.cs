using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAwayState : IBotState
{
    private BotStateMachine _stateMachine;
    private BotBRAIN _brain;
    private PlayerPickDrop _playerPickDrop;

    public void OnEnter(BotStateMachine botStateMachine)
    {
        _stateMachine = botStateMachine;
        _brain = _stateMachine.botBrain;
        _playerPickDrop = _brain.player.GetComponent<PlayerPickDrop>();
    }

    public void StateUpdate()
    {
        if (this == _stateMachine.currentState && _brain.player != null)
        {
            if (_stateMachine.IsInDanger())
            {
                _brain.FleeDanger();
            }
            else if (!_stateMachine.IsInDanger() && _brain.GetBombNumber() < _playerPickDrop.GetBombNumber())
            {
                _stateMachine.ChangeState(_stateMachine.searchBombState);
            }

            if (_brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && _brain.ClosestMenace() == _brain.player)
            {
                _stateMachine.ChangeState(_stateMachine.attackState);
            }
        }
    }

    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}
