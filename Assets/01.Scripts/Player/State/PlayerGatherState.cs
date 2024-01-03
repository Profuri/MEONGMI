using System;
using UnityEngine;

public class PlayerGatherState : PlayerState
{
    public PlayerGatherState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.PlayerHammer.AddAnimationEndEvent(GatheringFinishHandle);
        _player.PlayerHammer.SetAnimationSpeed(_player.PlayerStat.gatheringSpeed.GetValue());
        _player.PlayerHammer.GatheringTrigger();
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.PlayerHammer.RemoveAnimationEndEvent(GatheringFinishHandle);
        _player.PlayerHammer.ResetAnimationSpeed();
    }
    
    private void GatheringFinishHandle()
    {
        Debug.Log("!!");
        _player.OnHammerDownEvent?.Invoke(_player);
        _stateMachine.ChangeState(PlayerStateType.Idle);
    }
}