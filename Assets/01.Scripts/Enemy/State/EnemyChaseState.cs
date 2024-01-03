using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyState
{
    protected readonly int _speedHash = Animator.StringToHash("MOVESPEED");
    
    public EnemyChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        
    }

    public override void EnterState()
    {
        if (BaseEnemy.ActionData.IsStopped) return;
        
        BaseEnemy.StopImmediately();
        Vector3 lookPos = BaseEnemy.Target.position - BaseEnemy.transform.position;
        lookPos.y = 0f;
        BaseEnemy.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    
    
    public override void UpdateState()
    {
        if (BaseEnemy.ActionData.IsStopped) return;
        BaseEnemy.AnimatorCompo.SetFloat(_speedHash,BaseEnemy.NavMeshAgent.velocity.magnitude);
        
        Vector3 originPos = BaseEnemy.transform.position;
        float range = BaseEnemy.EnemyAttackSO.detectRange;
        int layer = BaseEnemy.LayerMask;
                
        var cols = Physics.OverlapSphere(originPos,range,layer);
        
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out IDetectable detectable))
                {
                    BaseEnemy.Target = detectable.Detect();
                    BaseEnemy.NavMeshAgent.SetDestination(BaseEnemy.Target.position);
                }
            }
        }
        
        float distance = Vector3.Distance(BaseEnemy.transform.position, BaseEnemy.Target.position);
        if (InAttackRange(distance))
        {
            _stateMachine.ChangeState(EEnemyState.Attack);
        }
        else
        {
            if (Vector3.Distance(BaseEnemy.NavMeshAgent.destination, BaseEnemy.Target.position) > 0.1f)
            {
                BaseEnemy.NavMeshAgent.SetDestination(BaseEnemy.Target.position);
            }
        }

    }

    public bool InAttackRange(float distance)
    {
        return distance <= BaseEnemy.EnemyAttackSO.attackRange;
    }
}
