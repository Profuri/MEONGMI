using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitState : State
{
    protected BaseTestMono BaseArea;
    protected BaseUnit BaseUnit;

    public UnitState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        BaseUnit = owner as BaseUnit;
        // TODO: Set Base Component
    }
}
