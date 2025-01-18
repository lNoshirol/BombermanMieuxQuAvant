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
        Debug.LogWarning($"RunAway {_stateMachine.IsInDanger() && _brain.GetBombNumber() <= _playerPickDrop.GetBombNumber()}");
        Debug.LogWarning($"SearchBomb {(_brain.GetBombNumber() == 0 || _brain.GetBombNumber() < _playerPickDrop.GetBombNumber()) && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count > 0}");
        Debug.LogWarning($"Attack {(_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger()) || (_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count == 0)}");

        if (this == _stateMachine.currentState && _brain.player != null)
        {
            _brain.FleeDanger();


            if (_stateMachine.IsInDanger() && _brain.GetBombNumber() <= _playerPickDrop.GetBombNumber() && !_brain.SuicidalMod())
            {
                _brain.FleeDanger();
            }
            else if ((_brain.GetBombNumber() == 0 || _brain.GetBombNumber() < _playerPickDrop.GetBombNumber()) && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count > 0)
            {
                _stateMachine.ChangeState(_stateMachine.searchBombState);
            }
            else if (((_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger()) ||
                    (_brain.GetBombNumber() > 0 && _brain.GetBombNumber() >= _playerPickDrop.GetBombNumber() && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count == 0)) || _brain.SuicidalMod())
            {
                _stateMachine.ChangeState(_stateMachine.attackState);
            }
        }
    }

    public void OnExit(BotStateMachine botStateMachine)
    {

    }
}
