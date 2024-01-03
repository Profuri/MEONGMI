using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackerChaseState : AttackerState
{

    public AttackerChaseState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        Debug.Log("Chase");
        Vector3 originPos = BaseUnit.transform.position;
        float range = _attackerStat.findArea;
        int layer = _attackerStat.targetLayer;

        var cols = Physics.OverlapSphere(originPos, range, layer);

        if (cols.Length > 0 && BaseUnit.Target == null)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out BaseEnemy enemy))
                {
                    BaseUnit.NavMesh.SetDestination(enemy.transform.position);
                    if(BaseUnit.NavMesh.path.status != NavMeshPathStatus.PathInvalid)
                    {
                        BaseUnit.SetTarget(enemy.transform);
                        break;
                    }
                }
            }
        }
        else if(cols.Length < 0)
        {
            _stateMachine.ChangeState(AttackerUnitStateType.Idle);
            return;
        }

        if (BaseUnit.Target == null)
        {
            _stateMachine.ChangeState(AttackerUnitStateType.Idle);
            return;
        }

        float distance = Vector3.Distance(BaseUnit.transform.position, BaseUnit.Target.position);
        if (distance < _attackerStat.attackRange)
        {
            _stateMachine.ChangeState(AttackerUnitStateType.Attack);
        }
        else
        {
            if (Vector3.Distance(BaseUnit.NavMesh.destination, BaseUnit.Target.position) > 0.1f)
            {
                BaseUnit.NavMesh.SetDestination(BaseUnit.Target.position);
            }
        }
    }
}
