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
        BaseEnemy.Target = GameManager.Instance.BaseTrm;
        _stateMachine.ChangeState(EEnemyState.Chase);
    }
}
