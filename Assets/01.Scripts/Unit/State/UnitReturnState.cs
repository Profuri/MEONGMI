using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitReturnState : UnitState
{
    public UnitReturnState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
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
    }
}
