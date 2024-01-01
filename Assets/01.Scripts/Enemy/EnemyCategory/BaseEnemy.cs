using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Default = 0,
    Assault = 1,
}

public abstract class BaseEnemy : Entity
{
    public NavMeshAgent NavMeshAgent { get; set; }
    public Transform Target { get; set; }
    public EnemyType EnemyType;
    
    public override void Awake()
    {
        base.Awake();
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        CharacterControllerCompo = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }
    
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
