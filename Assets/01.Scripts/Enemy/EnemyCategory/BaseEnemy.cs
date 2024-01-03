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
    Range = 2,
}

public abstract class BaseEnemy : Entity
{
    [SerializeField] private EnemyAttackSO _enemyAttackSO;
    public EnemyAttackSO EnemyAttackSO => _enemyAttackSO;

    public EntityStatSO EntityStatSo => _entityStatSO;
    public EnemyActionData ActionData { get; private set; }

    public EnemyAnimator EnemyAnimator { get; set; }
    public LayerMask LayerMask;
    
    public NavMeshAgent NavMeshAgent { get; set; }

    protected Coroutine _stopCoroutine;

    private Transform _cylinder;

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
        CurrentHP = _entityStatSO.maxHp;
        Transform visualTrm = transform.Find("Visual");
        Transform actionDataTrm = transform.Find("ActionData");
        _cylinder = transform.Find("Cylinder");

        ActionData = actionDataTrm.GetComponent<EnemyActionData>();
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        EnemyAnimator = visualTrm.GetComponent<EnemyAnimator>();
        CharacterControllerCompo = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        EnemyAnimator.Init(this,AnimatorCompo);
        ActionData.IsStopped = true;
        CharacterControllerCompo.enabled = false;
        
        _stateMachine.ChangeState(EEnemyState.Normal);
        
        EnemyAnimator.StartDissolveCor(1f,0f,1.2f, () =>
        {
            this.gameObject.SetActive(true);
            _cylinder.gameObject.SetActive(true);
            CharacterControllerCompo.enabled = true;
            NavMeshAgent.speed = EntityStatSo.moveSpeed;
            ActionData.IsStopped = false;
            NavMeshAgent.enabled = true;
            EnemyType = EnemyAttackSO.enemyType;
        });
        OnDead += DeadHandle;
    }
    
    protected override void RegisterStates()
    {
        foreach (EEnemyState eEnemyState in Enum.GetValues(typeof(EEnemyState)))
        {
            string typeName = $"Enemy{eEnemyState.ToString()}State";
            //Debug.Log($"TypeName: {typeName}");
            Type type = Type.GetType(typeName);
            EnemyState state = Activator.CreateInstance(type, _stateMachine, this, eEnemyState) as EnemyState;  
            _stateMachine.RegisterState(eEnemyState, state);
        }
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        EnemyAnimator.StartBlinkCoroutine(0f,1f,0.1f,null);
        //StopImmediately(true);
    }

    public void SetPosition(Vector3 pos)
    {
        NavMeshAgent.enabled = false;
        transform.position = pos;
        NavMeshAgent.enabled = true;
    }
    
    public void StopImmediately()
    {
        if (NavMeshAgent.enabled)
        {
            NavMeshAgent.SetDestination(transform.position);
        }
    }
    
    public void StopImmediately(bool isStopped)
    {
        ActionData.IsStopped = isStopped;
        NavMeshAgent.isStopped = isStopped;
    }
    
    protected override void SetInitState()
    {
        _stateMachine.Initialize(this,EEnemyState.Normal);
    }

    public void StartDelayCallBack(float time, Action Callback)
    {
        IEnumerator DelayCor(float time,Action Callback)
        {
            yield return new WaitForSeconds(time);
            Callback?.Invoke();
        }
        StartCoroutine(DelayCor(time, Callback));
    }

    protected virtual void DeadHandle()
    {
        _cylinder.gameObject.SetActive(false);
        CharacterControllerCompo.enabled = false;
        _stateMachine.ChangeState(EEnemyState.Dead);  
    }
}
