using UnityEngine;
using System;

public abstract class Entity : PoolableMono, IDamageable
{
    public Animator AnimatorCompo { get; protected set; }
    public CharacterController CharacterControllerCompo { get; protected set; }

    [SerializeField] protected EntityStatSO _entityStatSO;
    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;
    
    protected readonly int _hitHash = Animator.StringToHash("HIT");
    protected readonly int _deadHash = Animator.StringToHash("DEAD");

    protected float _maxHP;
    public float CurrentHP { get; protected set; }
    public float GetMaxHP() => _entityStatSO.maxHp;
    public bool Dead => CurrentHP <= 0;
    public event Action OnDead;
    
    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
        _maxHP = _entityStatSO.maxHp;
        CurrentHP = _maxHP;
    }

    public virtual void Start()
    {
        SetInitState();
    }

    public virtual void Update()
    {
        if (!Dead)
        {
            _stateMachine.CurrentState?.UpdateState();
        }
    }

    protected abstract void RegisterStates();
    protected abstract void SetInitState();
    
    public virtual void Damaged(float damage)
    {
        if (Dead)
        {
            return;
        }
        
        CurrentHP -= damage;
       // AnimatorCompo.SetTrigger(_hitHash);
        
        CurrentHP = Mathf.Clamp(CurrentHP, 0, _maxHP);
        
        if (CurrentHP <= 0)
        {
            AnimatorCompo.SetTrigger(_deadHash);
            OnDead?.Invoke();
        }
    }
}