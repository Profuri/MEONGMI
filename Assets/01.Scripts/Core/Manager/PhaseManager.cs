using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem.OnScreen;

public class PhaseManager : MonoSingleton<PhaseManager>
{
    [SerializeField] private float _restPhaseTime;
    public float RestPhaseTime => _restPhaseTime;

    [SerializeField] private List<PhaseInfoSO> _phaseInfoList = new List<PhaseInfoSO>();
    public List<PhaseInfoSO> PhaseInfoList => _phaseInfoList;

    public int Phase { get; private set; }

    private PhaseType _phase;
    public PhaseType PhaseType => _phase;
    
    private bool _phaseStart;

    private float _currentTime;
    public float GetCurTime() => _currentTime;  
    

    public event Action<PhaseType> OnPhaseChange;
    public event Action<int> OnPhaseTimer;
    
    public override void Init()
    {
        Phase = 0;
        PhaseStart();
    }
    
    public void PhaseStart()
    {
        _phaseStart = true;
        _phase = PhaseType.Rest;
    }

    public void PhaseStop()
    {
        _phaseStart = false;
    }

    public void Update()
    {
        if (!_phaseStart)
        {
            return;
        }
        
        if (_phase == PhaseType.Rest)
        {
            _currentTime += Time.deltaTime;
            OnPhaseTimer?.Invoke((int)(_restPhaseTime - _currentTime));
            
            if (_currentTime >= _restPhaseTime)
            {
                ChangePhase(PhaseType.Raid);
            }
        }
    }

    public void ChangePhase(PhaseType type)
    {
        _phase = type;
        OnPhaseChange?.Invoke(type);

        _currentTime = 0f;
        if (type == PhaseType.Raid)
        {
            Phase++;
            UIManager.Instance.ChangeUI("PhaseLogPanel", () =>
            {
                EnemySpawner.Instance.StartPhase(Phase);
            });
        }
    }
}

