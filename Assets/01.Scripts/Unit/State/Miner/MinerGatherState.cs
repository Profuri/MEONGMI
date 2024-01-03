using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class MinerGatherState : MinerState
{
    protected ResourceMono _currentRes;

    private float _startTime;

    public MinerGatherState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type) { }

    public override void EnterState()
    {
        base.EnterState();
        _currentRes = _minerUnit.TargetRes;
        if(_currentRes == null || _currentRes.IsInteractive || _currentRes.Invalid)
        {
            _minerUnit.SetTarget(null);
            _stateMachine.ChangeState(MinerUnitStateType.Move);
            return;
        }
        MinerState.SendStartGather(_currentRes);

        _startTime = Time.time;
        _currentRes.OnInteract(BaseUnit);
    }

    public override void ExitState()
    {
        base.ExitState();
        _minerUnit.SendEndGather();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Time.time > _startTime + _minerStat.gatherDuration)
        {
            _minerUnit.SetTarget(null);
            _stateMachine.ChangeState(MinerUnitStateType.Move);
        }
    }
}
