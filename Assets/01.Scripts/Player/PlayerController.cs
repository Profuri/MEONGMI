using System;
using System.Collections;
using InputControl;
using UnityEngine;

public class PlayerController : Entity, IDetectable
{
    [Range(0.1f, 1f)][SerializeField] private float _rotateSpeed;
    [SerializeField] private float _reviveTime = 2f;
    
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

    private Renderer[] _renderers;
    
    private static readonly int DissolveHash = Shader.PropertyToID("_Dissolve");

    public override void Awake()
    {
        
        _stateMachine = new StateMachine();
        RegisterStates();
        _maxHP = _playerStat.health.GetValue();
        CurrentHP = _maxHP;
        
        _lineConnect = GetComponent<PlayerLineConnect>();
        _visualTrm = transform.Find("Visual");

        _renderers = _visualTrm.GetComponentsInChildren<Renderer>(); 
        
        _walkParticle = _visualTrm.Find("WalkParticle").GetComponent<ParticleSystem>();
        PlayerHammer = _visualTrm.GetComponentInChildren<Hammer>();
        PlayerHammer.SetPlayerController(this);
        CharacterControllerCompo = GetComponent<CharacterController>();
        AnimatorCompo = _visualTrm.GetComponent<Animator>();
        OnDead += OnDeadHandle;
    }

    public override void Start()
    {
        _lineConnect.Init();
        base.Start();
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
        // particle
        // ResManager.Instance.
        StartCoroutine(PlayerDissolveRoutine(false, 0.5f));
        StartCoroutine(ReviveRoutine());
    }

    private IEnumerator ReviveRoutine()
    {
        yield return new WaitForSeconds(_reviveTime);
        yield return StartCoroutine(PlayerDissolveRoutine(true, 0.5f));
        CurrentHP = _maxHP;
        _lineConnect.Init();
        SetInitState();
    }

    private IEnumerator PlayerDissolveRoutine(bool generate, float time)
    {
        var cur = 0f;
        while (cur < time)
        {
            cur += Time.deltaTime;
            var percent = cur / time;
            Debug.LogWarning(percent);
            percent = generate ? 1f - percent : percent;
            
            foreach (var renderer in _renderers)
            {
                var matPropBlocks = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(matPropBlocks);
                matPropBlocks.SetFloat(DissolveHash, percent);
                renderer.SetPropertyBlock(matPropBlocks);
            }

            yield return null;
        }
        
        foreach (var renderer in _renderers)
        {
            var matPropBlocks = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(matPropBlocks);
            matPropBlocks.SetFloat(DissolveHash, generate ? 0f : 1f);
            renderer.SetPropertyBlock(matPropBlocks);
        }
    }

    public override void Init()
    {
        
    }

    public Transform Detect() => this.transform;
}