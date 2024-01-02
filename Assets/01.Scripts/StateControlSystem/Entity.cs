using UnityEngine;

public abstract class Entity : PoolableMono
{
    public Animator AnimatorCompo { get; protected set; }
    public CharacterController CharacterControllerCompo { get; protected set; }

    protected StateMachine _stateMachine;


    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        RegisterStates();
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
}