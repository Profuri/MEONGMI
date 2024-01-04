using System;
using UnityEngine;

public class PlayerChaseState : PlayerState
{
    public PlayerChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        _animToggleHash = Animator.StringToHash(PlayerStateType.Movement.ToString());
    }

    public override void EnterState()
    {
        base.EnterState();
        _player.InputReader.OnMouseLeftClickEvent += ClickHandle;
    }

    public override void UpdateState()
    {
        var dir = (_player.Target.transform.position - _player.transform.position).normalized;
        var distance = Vector3.Distance(_player.Target.transform.position, _player.transform.position);

        var input = _player.InputReader.movementInput;
        if (input.sqrMagnitude > 0.5f)
        {
            _player.Target = null;
            _stateMachine.ChangeState(PlayerStateType.Movement);
            return;
        }
        
        if (distance <= _player.Target.InteractRadius)
        {
            _stateMachine.ChangeState(PlayerStateType.Idle);
            _player.Rotate(dir, false);
            _player.Target.OnInteract(_player);
            return;
        }

        var speed = _player.PlayerStat.moveSpeed.GetValue() * Time.deltaTime;
        _player.SetVelocity(dir * speed);
        _player.Rotate(dir);
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.InputReader.OnMouseLeftClickEvent -= ClickHandle;
    }

    private void ClickHandle()
    {
        var clickObj = GetClickObject();

        if (clickObj == null)
        {
            return;
        }

        if (clickObj.TryGetComponent<Interactable>(out var interactable))
        {
            if (interactable != _player.Target)
            {
                _player.Target = interactable;
            }
        }
        else
        {
            _player.Target = null;
            _stateMachine.ChangeState(PlayerStateType.Idle);
        }
    }
    
    private Transform GetClickObject()
    {
        var mouseScreenPos = _player.InputReader.mouseScreenPos;
        var ray = Core.Define.MainCam.ScreenPointToRay(mouseScreenPos);
        var isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, _player.GroundMask | _player.InteractableMask);
        return isHit ? hit.transform : null;
    }
}