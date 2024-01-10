using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        CursorManager.Instance.OnBase();
        _player.InputReader.OnMouseLeftClickEvent += ClickHandle;
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

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            _stateMachine.ChangeState(PlayerStateType.Gather);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        _player.InputReader.OnMouseLeftClickEvent -= ClickHandle;
        _player.InputReader.OnMouseRightClickEvent -= ChargingHandle;
    }
    
    private void ChargingHandle(bool value)
    {
        if (value)
        {
            _stateMachine.ChangeState(PlayerStateType.Charging);
        }
    }
    
    private void ClickHandle()
    {
        var target = GetInteractObject();

        if (target != null)
        {
            _player.Target = target;
            _stateMachine.ChangeState(PlayerStateType.Chase);
        }
    }
    
    private Interactable GetInteractObject()
    {
        var mouseScreenPos = _player.InputReader.mouseScreenPos;
        var ray = CameraManager.Instance.MainCam.ScreenPointToRay(mouseScreenPos);
        var isHit = Physics.Raycast(ray, out var hit, Mathf.Infinity, _player.InteractableMask);
        return isHit ? hit.transform.GetComponent<Interactable>() : null;
    }
}