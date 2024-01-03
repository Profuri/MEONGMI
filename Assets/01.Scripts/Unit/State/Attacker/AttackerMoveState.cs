using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerMoveState : AttackerState
{
    public AttackerMoveState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
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

        Vector3 originPos = BaseUnit.transform.position;
        float range = _attackerStat.findArea;
        int layer = _attackerStat.targetLayer;

        var cols = Physics.OverlapSphere(originPos, range, layer);

        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out BaseEnemy enemy))
                {
                    BaseUnit.SetTarget(enemy.transform);
                    BaseUnit.NavMesh.SetDestination(BaseUnit.Target.position);
                    _stateMachine.ChangeState(AttackerUnitStateType.Chase);
                    return;
                }
            }
        }
    }
}
