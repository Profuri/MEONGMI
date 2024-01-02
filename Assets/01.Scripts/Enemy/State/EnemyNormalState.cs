using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyState
{
    public EnemyNormalState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        
    }

    public override void EnterState()
    {
        _stateMachine.ChangeState(EEnemyState.Chase);
    }
}
