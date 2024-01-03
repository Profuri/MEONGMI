using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected readonly int _deadHash = Animator.StringToHash("DEAD");
    public EnemyDeadState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        
    }

    public override void EnterState()
    {
        BaseEnemy.ActionData.IsStopped = true;
        BaseEnemy.NavMeshAgent.speed = 0f;
        BaseEnemy.StopImmediately();
        BaseEnemy.NavMeshAgent.enabled = false;

        BaseEnemy.EnemyAnimator.OnDeadEvent += TempDissolve;
    }

    public override void ExitState()
    {
        BaseEnemy.EnemyAnimator.OnDeadEvent -= TempDissolve;
    }

    private void TempDissolve()
    {
        BaseEnemy.EnemyAnimator.StartDissolveCor(0f,1f,0.8f,() => PoolManager.Instance.Push(BaseEnemy));      
    }
}
