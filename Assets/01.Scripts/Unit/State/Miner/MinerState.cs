using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerState : UnitState
{
    protected List<Transform> _resourceRefs;

    public MinerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        // TODO: Set resourceRefs from Base Component
    }
}
