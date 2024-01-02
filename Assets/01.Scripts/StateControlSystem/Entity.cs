using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Animator AnimatorCompo { get; private set; }
    public CharacterController CharacterControllerCompo { get; private set; }

    protected StateMachine _stateMachine;
    public StateMachine StateMachine => _stateMachine;

    public virtual void Awake()
    {
        _stateMachine = new StateMachine();
        var visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        CharacterControllerCompo = GetComponent<CharacterController>();
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