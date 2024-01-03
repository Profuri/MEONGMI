using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerIdleState : AttackerState
{
    public AttackerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}
