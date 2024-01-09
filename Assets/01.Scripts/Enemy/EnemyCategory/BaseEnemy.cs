using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
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
    protected Coroutine _debuffCoroutine;

    private Transform _cylinder;

    private Transform _target;

    //나중에 확장성 있게 debuffManager를 만들어서 처리해주면 좋을듯
    [Header("Hit")]
    [SerializeField] private BulletType _curHitBullet;

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
            NavMeshAgent.speed = _entityStatSO.moveSpeed;
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

    public override void Damaged(DamageType type, float damage)
    {
        EnemyAnimator.StartBlinkCoroutine(0f,1f,0.1f, () => StopAllCoroutines());
        //StopImmediately(true);
        base.Damaged(type, damage);
    }

    // 총 전환 생기면 스크립트 바꿔야 함.
    public void SetDebuff(BulletType type, float damage)
    {
        _curHitBullet = type;

        Debug.Log("set");
        if (_debuffCoroutine != null)
        {
            //StopCoroutine(_debuffCoroutine);
            _debuffCoroutine = null;
        }
        //StopAllCoroutines(); //poison이 두개의 코루틴을 사용하기에.
        
        float duration;
        switch (type)
        {
            case BulletType.Slow:
                duration = BulletDebuffManager.Instance.SlowDuration;
                float slowPercent = BulletDebuffManager.Instance.SlowPercent;
                NavMeshAgent.speed = _entityStatSO.moveSpeed * slowPercent;

                //_debuffCoroutine = StartCoroutine(DelayCor(duration,
                GameManager.Instance.StartCoroutine(DelayCor(duration, Callback: ReSetSpeed));
                    

                break;
            case BulletType.Poison:

                duration = BulletDebuffManager.Instance.PoisonApplyDuration;
                //_debuffCoroutine = StartCoroutine(PoisonCor(duration,damage));
                GameManager.Instance.StartCoroutine(PoisonCor(duration,damage));
                  break;
           
            case BulletType.Pierce:
            case BulletType.Missile:
            case BulletType.Transition:
            case BulletType.Material:
            case BulletType.Enemy:
                break;
        }
    }

    void ReSetSpeed()
    {
        Debug.Log("Reset");
        NavMeshAgent.speed = _entityStatSO.moveSpeed;
    }

    IEnumerator PoisonCor(float duration, float damage)
    {
        int poisonTickCount = BulletDebuffManager.Instance.PoisonTickCount;
        float TickDamagePercent = BulletDebuffManager.Instance.TickDamagePercent;
       // Debug.Log( "PoisonTIck : " + poisonTickCount);
        
        for (int i = 0; i < poisonTickCount; i++)
        {
            float temp = (float)duration / (float)poisonTickCount;
            //Debug.Log("temp: " + temp);
            //Debug.Log($"{this.gameObject.name} : Poison {i}");
            yield return new WaitForSeconds(0.5f);
            Damaged(DamageType.None, damage * TickDamagePercent);
        }
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
        StartCoroutine(DelayCor(time, Callback));
    }

    IEnumerator DelayCor(float time, Action Callback)
    {
        Debug.Log("Start");
        yield return new WaitForSeconds(time);
        Debug.Log("end");
        Callback?.Invoke();
    }

    protected virtual void DeadHandle()
    {
        _cylinder.gameObject.SetActive(false);
        CharacterControllerCompo.enabled = false;
        _stateMachine.ChangeState(EEnemyState.Dead);  
    }
}
