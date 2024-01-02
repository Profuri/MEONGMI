using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Editor;
using UnityEngine.Serialization;

public enum EnemyType
{
    Default = 0,
    Assault = 1,
}

public abstract class BaseEnemy : Entity
{
    [SerializeField] private EnemyAttackSO _enemyAttackSO;
    public EnemyAttackSO EnemyAttackSO => _enemyAttackSO;

    public EntityStatSO EntityStatSo => _entityStatSO;

    public LayerMask LayerMask;
    
    public NavMeshAgent NavMeshAgent { get; set; }

    private Transform _target;
    public Transform Target
    {
        get
        {
            if (_target == null)
            {
                _target = GameManager.Instance.BaseTrm;
            }

            return _target;
        }
        set => _target = value;
    }
    
    public EnemyType EnemyType;

    public sealed override void Init() 
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        CharacterControllerCompo = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        NavMeshAgent.speed = EntityStatSo.moveSpeed;
        EnemyType = EnemyAttackSO.enemyType;
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

    public void StopImmediately()
    {
        NavMeshAgent?.SetDestination(transform.position);
    }
}
