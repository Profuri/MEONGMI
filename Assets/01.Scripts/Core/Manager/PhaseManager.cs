using System.Collections.Generic;
using UnityEngine;
public class PhaseManager : MonoSingleton<PhaseManager>
{
    [SerializeField] private float _restPhaseTime;
    [SerializeField] private float _raidPhaseTime;
    
    [SerializeField] 
    private List<PhaseInfoSO> _phaseInfoList = new List<PhaseInfoSO>();
    public List<PhaseInfoSO> PhaseInfoList => _phaseInfoList;
    
    public int Phase { get; private set; }
    
    private PhaseType _phase;
    private bool _phaseStart;

    private float _currentTime;

    private void Awake()
    {
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

        _currentTime += Time.deltaTime;

        if (_phase == PhaseType.Rest)
        {
            if (_currentTime >= _restPhaseTime)
            {
                ChangePhase(PhaseType.Raid);
            }
        }
        else
        {
            if (_currentTime >= _raidPhaseTime)
            {
                ChangePhase(PhaseType.Rest);
            }
        }
    }

    public void ChangePhase(PhaseType type)
    {
        _phase = type;
    }

    public override void Init()
    {
        Phase = 0;
        EnemySpawner.Instance.StartPhase(0);
        
    }
}