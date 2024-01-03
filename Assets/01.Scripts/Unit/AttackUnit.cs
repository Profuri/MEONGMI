using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnit : BaseUnit
{
    public override void Init()
    {
        
    }

    protected override void RegisterStates()
    {
        foreach (AttackerUnitStateType stateType in Enum.GetValues(typeof(AttackerUnitStateType)))
        {
            var typeName = $"Attacker{stateType.ToString()}State";
            var type = Type.GetType(typeName);
            var state = Activator.CreateInstance(type, _stateMachine, this, stateType) as AttackerState;
            _stateMachine.RegisterState(stateType, state);
        }
    }

    protected override void SetInitState()
    {
        
    }
}
