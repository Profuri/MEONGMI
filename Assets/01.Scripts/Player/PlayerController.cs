using System;
using InputControl;
using UnityEngine;

public class PlayerController : Entity
{
    [Range(0.1f, 1f)][SerializeField] private float _rotateSpeed;
    
    [SerializeField] private PlayerStat _playerStat;
    public PlayerStat PlayerStat => _playerStat;

    [SerializeField] private InputReader _inputReader;
    public InputReader InputReader => _inputReader;

    [SerializeField] private LayerMask _groundMask;
    public LayerMask GroundMask => _groundMask;

    private Transform _visualTrm;

    public override void Awake()
    {
        base.Awake();
        _visualTrm = transform.Find("Visual");
    }

    public void SetVelocity(Vector3 dir)
    {
        CharacterControllerCompo.Move(dir);
    }

    public void Rotate(Vector3 dir)
    {
        var currentRotation = _visualTrm.rotation;
        var destRotation = Quaternion.LookRotation(dir);
        _visualTrm.rotation = Quaternion.Lerp(currentRotation, destRotation, _rotateSpeed);
    }
    
    public void StopImmediately()
    {
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
}