using System;
using UnityEngine;

public class PlayerState : State
{
    protected PlayerController _player => _owner as PlayerController;
    
    public PlayerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    
    }

    public override void UpdateState()
    {
        // var groundPoint = GetGroundPoint();
        // if (groundPoint != -Vector3.one)
        // {
        //     var lookDir = (groundPoint - _player.transform.position).normalized;
        //     _player.Rotate(lookDir);
        // }
    }

    private Vector3 GetGroundPoint()
    {
        var mouseScreenPos = _player.InputReader.mouseScreenPos;
        var ray = GameManager.Instance.MainCam.ScreenPointToRay(mouseScreenPos);
        var isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, _player.GroundMask);
        return isHit ? hit.point : -Vector3.one;
    }
}