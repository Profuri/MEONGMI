using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerIdleState : AttackerState
{
    public AttackerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _stateMachine.ChangeState(AttackerUnitStateType.Move);
    }
}
