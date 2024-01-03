using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerChaseState : AttackerState
{
    public AttackerChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}
