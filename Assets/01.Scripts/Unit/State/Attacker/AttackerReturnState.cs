using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerReturnState : AttackerState
{
    public AttackerReturnState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}
