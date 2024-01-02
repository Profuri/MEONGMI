using System;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public PlayerMovementState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
    
    public override void EnterState()
    {
        base.EnterState();
        _player.InputReader.OnMouseRightClickEvent += ChargingHandle;
    }
    
    public override void UpdateState()
    {
        var movementInput = Quaternion.Euler(0, 45, 0) * _player.InputReader.movementInput;

        if (movementInput.sqrMagnitude < 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateType.Idle);
            return;
        }

        var movementSpeed = _player.PlayerStat.moveSpeed.GetValue() * Time.deltaTime;
        _player.SetVelocity(movementInput * movementSpeed);
        _player.Rotate(movementInput);
    }
    
    public override void ExitState()
    {
        base.ExitState();
        _player.InputReader.OnMouseRightClickEvent -= ChargingHandle;
    }

    private void ChargingHandle()
    {
        _stateMachine.ChangeState(PlayerStateType.Charging);    
    }
}