using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinerMoveState : MinerState
{
    public MinerMoveState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        OnStartGather += HandleStartGather;
    }

    public override void ExitState()
    {
        base.ExitState();
        OnStartGather -= HandleStartGather;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(_minerUnit.TargetRes == null)
        {
            if(FindResource(out ResourceMono res) && !res.IsInteractive && !res.Invalid)
            {
                _minerUnit.SetTarget(res);
                return;
            }

            if (Vector3.Distance(_minerUnit.transform.position, _minerUnit.HoldPosition) < 0.3f)
            {
                _stateMachine.ChangeState(MinerUnitStateType.Idle);
                return;
            }
            _minerUnit.NavMesh.SetDestination(_minerUnit.HoldPosition);
        }
        else
        {
            if(_minerUnit.TargetRes.IsInteractive)
            {
                _minerUnit.SetTarget(null);
                _minerUnit.NavMesh.SetDestination(_minerUnit.HoldPosition);
                return;
            }

            if (Vector3.Distance(_minerUnit.transform.position, _minerUnit.TargetRes.transform.position) < 1f)
            {
                _stateMachine.ChangeState(MinerUnitStateType.Gather);
                return;
            }

            _minerUnit.NavMesh.SetDestination(_minerUnit.TargetRes.transform.position);
            if(_minerUnit.NavMesh.path.status == NavMeshPathStatus.PathInvalid)
            {
                _minerUnit.TargetRes.Invalid = true;
            }
        }
    }
}
