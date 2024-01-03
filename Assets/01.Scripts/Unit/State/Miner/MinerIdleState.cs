using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerIdleState : MinerState
{
    public MinerIdleState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        OnStartGather += HandleStartGather;
    }

    public override void ExitState()
    {
        base.ExitState();
        OnStartGather -= HandleStartGather;
    }

    public override void UpdateState()
    {
        if(_minerUnit.TargetRes == null)
        {
            // TODO: ±¤¸Æ Å½Áö
            if(FindResource(out ResourceMono res) && !res.IsInteractive && !res.Invalid)
            {
                _minerUnit.SetTarget(res);
                _stateMachine.ChangeState(MinerUnitStateType.Move);
                return;
            }

            if(Vector3.Distance(_minerUnit.transform.position, _minerUnit.HoldPosition) > 0.3f)
            {
                _stateMachine.ChangeState(MinerUnitStateType.Move);
            }
        }
        else
        {
            _stateMachine.ChangeState(MinerUnitStateType.Move);
        }
    }
}
