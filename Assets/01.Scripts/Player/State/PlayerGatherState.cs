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
        CameraManager.Instance.ImpulseCam(0.25f, 0.15f, new Vector3(0, -1, 0));

        var particle = PoolManager.Instance.Pop("GatheringParticle") as PoolableParticle;
        particle.SetPositionAndRotation(_player.Target.transform.position);
        particle.Play();

        _player.OnHammerDownEvent?.Invoke(_player);
        _stateMachine.ChangeState(PlayerStateType.Idle);
    }
}