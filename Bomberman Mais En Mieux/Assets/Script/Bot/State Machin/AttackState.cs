using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IBotState
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
            _brain.AttackPlayer();

            if (_brain.SuicidalMod()) return;

            if (_stateMachine.IsInDanger() && _brain.GetBombNumber() <= _playerPickDrop.GetBombNumber())
            {
                _stateMachine.ChangeState(_stateMachine.runAwayState);
            }
            else if ((_brain.GetBombNumber() == 0 || _brain.GetBombNumber() < _playerPickDrop.GetBombNumber()) && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count > 0)
            {
                _stateMachine.ChangeState(_stateMachine.searchBombState);
            }
            else if ((_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger()) || 
                    (_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count == 0))
            {
                _brain.AttackPlayer();
            }
        }
    }


    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}
