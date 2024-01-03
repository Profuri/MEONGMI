using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerAttackState : AttackerState
{
    private float _lastAttackTime;

    public AttackerAttackState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Attack Enter");
        if(!_attackerUnit.Target.gameObject.activeSelf)
        {
            _attackerUnit.SetTarget(null);
            _stateMachine.ChangeState(AttackerUnitStateType.Idle);
            Debug.Log("No Target");
            return;
        }
        if(Time.time > _lastAttackTime + _attackerStat.attackDuration)
        {
            BaseUnit.StopImmediately();
            _lastAttackTime = Time.time;
            if(BaseUnit.Target.TryGetComponent(out BaseEnemy enemy))
            {
                if(!enemy.Dead)
                {
                    enemy.Damaged(_attackerStat.damage);
                }
                _stateMachine.ChangeState(AttackerUnitStateType.Chase);
            }
        }
        else
        {
            _stateMachine.ChangeState(AttackerUnitStateType.Chase);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
