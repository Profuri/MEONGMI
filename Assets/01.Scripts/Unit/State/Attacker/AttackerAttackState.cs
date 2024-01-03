using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAttackState : AttackerState
{
    public AttackerAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}
