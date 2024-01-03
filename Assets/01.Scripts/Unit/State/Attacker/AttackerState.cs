using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackerState : UnitState
{
    protected Vector3 _holdPosition;
    protected Vector3 _destination;

    protected AttackerStatSO _attackerStat;

    public AttackerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        _attackerStat = BaseUnit.UnitStatSO as AttackerStatSO;
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
        if(Vector3.Distance(BaseUnit.transform.position, _destination) < 0.3f)
        {
            float rand = Random.Range(0f, Mathf.PI * 2f);
            Vector3 normal = new Vector3(Mathf.Cos(rand), 0f, Mathf.Sin(rand));
            _destination = _holdPosition + normal * _attackerStat.holdRange;

            BaseUnit.NavMesh.SetDestination(_destination);
        }
    }
}
