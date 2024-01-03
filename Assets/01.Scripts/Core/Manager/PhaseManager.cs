using UnityEngine;

public class PhaseManager : MonoSingleton<PhaseManager>
{
    [SerializeField] private float _restPhaseTime;
    [SerializeField] private float _raidPhaseTime;

    private PhaseType _phase;
    private bool _phaseStart;

    private float _currentTime;
    public float GetCurTime() => _currentTime;  
    

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
    }
}