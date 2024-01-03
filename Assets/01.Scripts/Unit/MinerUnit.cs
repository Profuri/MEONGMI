using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerUnit : BaseUnit
{
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
        _stateMachine.ChangeState(MinerUnitStateType.Idle);
    }
}
