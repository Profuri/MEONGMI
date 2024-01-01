using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Entity
{
    protected override void RegisterStates()
    {
        foreach (EEnemyState eEnemyState in Enum.GetValues(typeof(EEnemyState)))
        {
            string typeName = eEnemyState.ToString();
            Type type = Type.GetType($"Enemy{typeName}State");
            EnemyState state = Activator.CreateInstance(type,this,_stateMachine,typeName) as EnemyState;

            _stateMachine.RegisterState(eEnemyState,state);
        }
    }

    protected override void SetInitState()
    {
        _stateMachine.Initialize(this,EEnemyState.Normal);
    }
}
