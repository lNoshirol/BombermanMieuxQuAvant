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
        if(_brain.player != null)
        {
            _playerPickDrop = _brain.player.GetComponent<PlayerPickDrop>();
        }
        
    }

    public void StateUpdate()
    {
        if (this == _stateMachine.currentState && _brain.player != null)
        {
            _brain.BOTFindNearestBombe();


            if (_stateMachine.IsInDanger() && _brain.GetBombNumber() <= _playerPickDrop.GetBombNumber() && !_brain.SuicidalMod())
            {
                _stateMachine.ChangeState(_stateMachine.runAwayState);
            }
            else if ((_brain.GetBombNumber() == 0 || _brain.GetBombNumber() < _playerPickDrop.GetBombNumber()) && !_stateMachine.IsInDanger() && _brain.AllBombesList.Count > 0)
            {
                _brain.BOTFindNearestBombe();
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