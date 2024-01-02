using System;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.StopImmediately();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        var movementInput = _player.InputReader.movementInput;
        if (movementInput.sqrMagnitude > 0.05f)
        {
            _stateMachine.ChangeState(PlayerStateType.Movement);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}