using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State
{
    public EnemyState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }
}
