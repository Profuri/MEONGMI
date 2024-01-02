using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        BaseEnemy.NavMeshAgent.SetDestination(BaseEnemy.Target.position);
    }
    
    public override void UpdateState()
    {
        Vector3 originPos = BaseEnemy.transform.position;
        float range = BaseEnemy.EnemyAttackSO.detectRange;
        int layer = BaseEnemy.LayerMask;
            
        var cols = Physics.OverlapSphere(originPos,range,layer);
        float distance = Vector3.Distance(BaseEnemy.transform.position, BaseEnemy.Target.position);
        if (InAttackRange(distance))
        {
            _stateMachine.ChangeState(EEnemyState.Attack);
            return;
        }
        
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out IDetectable detectable))
                {
                    BaseEnemy.Target = detectable.Detect();
                    _stateMachine.ChangeState(EEnemyState.Chase);
                    Debug.Log($"CurrentTarget: {BaseEnemy.Target}");
                    return;
                }
            }
        }
        else
        {
            BaseEnemy.Target = GameManager.Instance.BaseTrm;
        }
        

    }

    public bool InAttackRange(float distance)
    {
        return distance <= BaseEnemy.EnemyAttackSO.attackRange;
    }
}
