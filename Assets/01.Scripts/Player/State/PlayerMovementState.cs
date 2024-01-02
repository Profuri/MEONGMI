using System;
using UnityEngine;

public class PlayerMovementState : PlayerState
{
    public PlayerMovementState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
    
    public override void UpdateState()
    {
        var movementInput = _player.InputReader.movementInput;

        if (movementInput.sqrMagnitude < 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateType.Idle);
        }

        var movementSpeed = _player.PlayerStat.moveSpeed.GetValue();
        _player.SetVelocity(new Vector3(movementInput.x, 0, movementInput.y) * movementSpeed);
    }
}