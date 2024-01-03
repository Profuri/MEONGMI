using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerState : UnitState
{
    public static event Action<ResourceMono> OnStartGather;
    protected MinerStatSO _minerStat;

    protected MinerUnit _minerUnit => BaseUnit as MinerUnit;

    public MinerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
        _minerStat = BaseUnit.UnitStatSO as MinerStatSO;
    }

    protected bool FindResource(out ResourceMono res)
    {
        res = null;

        RaycastHit[] hits = Physics.SphereCastAll(BaseUnit.transform.position,
                                                  _minerStat.holdRange,
                                                  BaseUnit.transform.forward,
                                                  _minerStat.holdRange,
                                                  BaseUnit.UnitStatSO.targetLayer);
        if (hits.Length < 0) return false;

        foreach(var hit in hits)
        {
            if(hit.transform.TryGetComponent(out ResourceMono resource) && !resource.IsInteractive && !resource.Invalid)
            {
                res = resource;
                return true;
            }
        }

        return false;
    }

    protected void HandleStartGather(ResourceMono res)
    {
        if (res == _minerUnit.TargetRes)
        {
            BaseUnit.SetTarget(null);
            BaseUnit.NavMesh.SetDestination(BaseUnit.HoldPosition);
            _stateMachine.ChangeState(MinerUnitStateType.Move);
        }
    }

    protected static void SendStartGather(ResourceMono res)
    {
        OnStartGather?.Invoke(res);
    }
}
