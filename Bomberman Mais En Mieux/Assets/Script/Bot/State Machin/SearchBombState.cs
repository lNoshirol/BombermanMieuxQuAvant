using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBombState : IBotState
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
            if (_brain.GetBombNumber() == 0 || (_brain.GetBombNumber() < _playerPickDrop.GetBombNumber()))
            {
                _brain.BOTFindNearestBombe();
            }
            else if (_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber())
            {
                _stateMachine.ChangeState(_stateMachine.attackState);
            }
            else if (_stateMachine.IsInDanger())
            {
                _stateMachine.ChangeState(_stateMachine.runAwayState);
            }
        }
    }

    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}