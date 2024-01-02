using System;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.InputReader.OnMouseRightClickEvent += ChargingHandle;
        _player.StopImmediately();
    }

    public override void UpdateState()
    {
        var movementInput = _player.InputReader.movementInput;
        if (movementInput.sqrMagnitude > 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateType.Movement);
        }
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