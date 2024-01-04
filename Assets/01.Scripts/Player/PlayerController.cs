using System;
using InputControl;
using UnityEngine;

public class PlayerController : Entity, IDetectable
{
    [Range(0.1f, 1f)][SerializeField] private float _rotateSpeed;
    
    [SerializeField] private PlayerStat _playerStat;
    public PlayerStat PlayerStat => _playerStat;

    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    [SerializeField] private LayerMask _groundMask;
    public LayerMask GroundMask => _groundMask;

    [SerializeField] private LayerMask _interactableMask;
    public LayerMask InteractableMask => _interactableMask;

    [SerializeField] private BulletType _bulletType;
    public BulletType BulletType => _bulletType;

    private PlayerLineConnect _lineConnect;
    private ParticleSystem _walkParticle;
    
    public PlayerLineConnect LineConnect
    {
        get
        {
            if (_lineConnect == null)
            {
                _lineConnect = GetComponent<PlayerLineConnect>();
            }

            return _lineConnect;
        }
    }
    public Hammer PlayerHammer { get; private set; }
    public Interactable Target { get; set; }

    public Action OnHammerDownEvent;
    
    private Transform _visualTrm;

    public override void Awake()
    {
        base.Awake();
        _lineConnect = GetComponent<PlayerLineConnect>();
        _visualTrm = transform.Find("Visual");
        _walkParticle = _visualTrm.Find("WalkParticle").GetComponent<ParticleSystem>();
        PlayerHammer = _visualTrm.GetComponentInChildren<Hammer>();
        PlayerHammer.SetPlayerController(this);
        CharacterControllerCompo = GetComponent<CharacterController>();
        AnimatorCompo = _visualTrm.GetComponent<Animator>();
        OnDead += OnDeadHandle;
    }

    public void SetBullet(BulletType type)
    {
        _bulletType = type;
    }

    public void SetVelocity(Vector3 dir)
    {
        if (!_walkParticle.isPlaying)
        {
            _walkParticle.Play();
        }
        
        CharacterControllerCompo.Move(dir);
    }

    public void Rotate(Vector3 dir, bool lerp = true)
    {
        var currentRotation = _visualTrm.rotation;
        var destRotation = Quaternion.LookRotation(dir);

        if (lerp)
        {
            _visualTrm.rotation = Quaternion.Lerp(currentRotation, destRotation, _rotateSpeed);
        }
        else
        {
            _visualTrm.rotation = destRotation;
        }
    }
    
    public void StopImmediately()
    {
        _walkParticle.Stop();
        CharacterControllerCompo.Move(Vector3.zero);
    }
    
    protected override void RegisterStates()
    {
        foreach (PlayerStateType stateType in Enum.GetValues(typeof(PlayerStateType)))
        {
            var typeName = $"Player{stateType.ToString()}State";
            var type = Type.GetType(typeName);
            var state = Activator.CreateInstance(type, _stateMachine, this, stateType) as PlayerState;  
            _stateMachine.RegisterState(stateType, state);
        }
    }

    protected override void SetInitState()
    {
        _stateMachine.Initialize(this, PlayerStateType.Idle);
    }

    public override void Damaged(float damage)
    {
        base.Damaged(damage);
        TestUIManager.Instance.IngameUI.UpdatePlayerHP();
    }

    public void SetAnimationSpeed(float speed)
    {
        AnimatorCompo.speed = speed;
    }

    public void ResetAnimationSpeed()
    {
        AnimatorCompo.speed = 1f;
    }

    private void OnDeadHandle()
    {
        // ResManager.Instance.
    }

    public override void Init()
    {
        
    }

    public Transform Detect() => this.transform;
}