using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private EnemyAttack _enemyAttack;    
    public EnemyAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        _enemyAttack = owner.transform.Find("Attack").GetComponent<EnemyAttack>();
        _enemyAttack.Init(BaseEnemy);
    }

    public override void EnterState()
    {
        Debug.Log("OnEnterEnemyAttackState");
        BaseEnemy.StopImmediately();
    }

    public override void UpdateState()
    {
        if (_enemyAttack.CanAttack())
        {
            _enemyAttack?.Attack();
            _stateMachine.ChangeState(EEnemyState.Chase);
        }
    }
    
    
}
