using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : Entity
{
    public NavMeshAgent NavMeshAgent { get; set; }

    protected override void RegisterStates()
    {
        foreach (EEnemyState eEnemyState in Enum.GetValues(typeof(EEnemyState)))
        {
            string typeName = $"Enemy{eEnemyState.ToString()}State";
            Debug.Log($"TypeName: {typeName}");
            Type type = Type.GetType(typeName);
            EnemyState state = Activator.CreateInstance(type, _stateMachine, this, eEnemyState) as EnemyState;  
            _stateMachine.RegisterState(eEnemyState, state);
        }
    }

    protected override void SetInitState()
    {
        _stateMachine.Initialize(this,EEnemyState.Normal);
    }
}
