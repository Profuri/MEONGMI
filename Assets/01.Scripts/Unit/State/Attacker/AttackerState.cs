using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerState : UnitState
{
    protected Vector3 _destination;

    protected AttackerStatSO _attackerStat;

    protected AttackerUnit _attackerUnit => BaseUnit as AttackerUnit;

    protected int _speedHash = Animator.StringToHash("speed");

    public AttackerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        _attackerStat = BaseUnit.UnitStatSO as AttackerStatSO;
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
