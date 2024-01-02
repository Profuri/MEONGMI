using System;
using UnityEngine;

public class PlayerShotState : PlayerState
{
    private float _lastShotTime;
    
    public PlayerShotState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        if (Time.time < _lastShotTime + _player.PlayerStat.shotDelay.GetValue())
        {
            _stateMachine.ChangeState(PlayerStateType.Charging);
            return;
        }
        
        _player.PlayerHammer.ShotTrigger();
        Shot();
        
        _lastShotTime = Time.time;
        _stateMachine.ChangeState(PlayerStateType.Charging);
    }

    public override void ExitState()
    {
    }

    private void Shot()
    {
        var groundPoint = GetGroundPoint();
        var dir = (groundPoint - _player.transform.position).normalized;
        _player.PlayerHammer.Shot(BulletType.Pierce, dir);
    }
    
    private Vector3 GetGroundPoint()
    {
        var mouseScreenPos = _player.InputReader.mouseScreenPos;
        var ray = GameManager.Instance.MainCam.ScreenPointToRay(mouseScreenPos);
        var isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, _player.GroundMask);
        return isHit ? hit.point : -Vector3.one;
    }
}