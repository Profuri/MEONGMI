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

    public int CurrentHP { get; protected set; }
    public bool Dead => CurrentHP <= 0;
    public event Action OnDead;
    
    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
        CurrentHP = _entityStatSO.hp;
    }

    public virtual void Start()
    {
        SetInitState();
    }

    public virtual void Update()
    {
        _stateMachine.CurrentState?.UpdateState();
    }

    protected abstract void RegisterStates();
    protected abstract void SetInitState();
    
    public virtual void Damaged(int damage)
    {
        CurrentHP -= damage;
        AnimatorCompo.SetTrigger(_hitHash);
        
        CurrentHP = Mathf.Clamp(CurrentHP, 0,_entityStatSO.hp);
        if (CurrentHP == 0)
        {
            AnimatorCompo.SetTrigger(_deadHash);
            OnDead?.Invoke();
        }
    }
    
}