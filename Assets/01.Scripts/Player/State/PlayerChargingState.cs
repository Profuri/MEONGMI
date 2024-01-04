using System;
using UnityEngine;

public class PlayerChargingState : PlayerState
{
    public PlayerChargingState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        CameraManager.Instance.Zoom(9.5f, 0.2f);
        _player.InputReader.OnMouseLeftClickEvent += ShotHandle;
        _player.InputReader.OnMouseRightClickEvent += ChargingHandle;
        _player.PlayerHammer.ChargingToggle(true);
    }

    public override void UpdateState()
    {
        var groundPoint = GetGroundPoint();
        var movementInput = Quaternion.Euler(0, 45, 0) * _player.InputReader.movementInput;
        var movementSpeed = _player.PlayerStat.chargingSpeed.GetValue() * Time.deltaTime;

        if (movementInput.sqrMagnitude > 0.05f)
        {
            _player.SetVelocity(movementInput * movementSpeed);
        }
        else
        {
            _player.StopImmediately();
        }
        
        if (groundPoint != -Vector3.one)
        {
            var lookDir = (groundPoint - _player.transform.position).normalized;
            lookDir.y = 0;
            _player.Rotate(lookDir);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.InputReader.OnMouseLeftClickEvent -= ShotHandle;
        _player.InputReader.OnMouseRightClickEvent -= ChargingHandle;
    }
    
    private Vector3 GetGroundPoint()
    {
        var mouseScreenPos = _player.InputReader.mouseScreenPos;
        var ray = Core.Define.MainCam.ScreenPointToRay(mouseScreenPos);
        var isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, _player.GroundMask);
        return isHit ? hit.point : -Vector3.one;
    }

    private void ChargingHandle()
    {
        CameraManager.Instance.Zoom(10, 0.2f);
        _stateMachine.ChangeState(PlayerStateType.Idle);
        _player.PlayerHammer.ChargingToggle(false);
    }

    private void ShotHandle()
    {
        _stateMachine.ChangeState(PlayerStateType.Shot);
    }
}