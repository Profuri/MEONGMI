using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerUnit : BaseUnit
{
    public event Action<Entity> EndGather;

    public ResourceMono TargetRes;

    public override void Init()
    {
        _unitType = UnitType.Miner;
    }

    protected override void RegisterStates()
    {
        foreach (MinerUnitStateType stateType in Enum.GetValues(typeof(MinerUnitStateType)))
        {
            var typeName = $"Miner{stateType.ToString()}State";
            var type = Type.GetType(typeName);
            var state = Activator.CreateInstance(type, _stateMachine, this, stateType) as MinerState;
            _stateMachine.RegisterState(stateType, state);
        }
    }

    protected override void SetInitState()
    {
        _stateMachine.Initialize(this, MinerUnitStateType.Idle);
    }

    public void SendEndGather()
    {
        EndGather?.Invoke(this);
    }

    public void SetTarget(ResourceMono res)
    {
        TargetRes = res;
        if(res != null)
        base.SetTarget(res.transform);
    }
}
