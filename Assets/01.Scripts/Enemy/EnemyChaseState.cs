using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    public Transform Target { get; set; }
    

    public EnemyChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        
    }

    public override void UpdateState()
    {
        BaseEnemy.NavMeshAgent.SetDestination(Target.position);
    }
}
