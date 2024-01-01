using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        
    }

    public override void EnterState()
    {
        Debug.Log("EnterChaseState");
    }
    public override void UpdateState()
    {
        Debug.Log(BaseEnemy);
        Debug.Log(BaseEnemy.Target);
        BaseEnemy.NavMeshAgent.SetDestination(BaseEnemy.Target.position);
    }
}
