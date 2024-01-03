using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerMoveState : MinerState
{
    public MinerMoveState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}
