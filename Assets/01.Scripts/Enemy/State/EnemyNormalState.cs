using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyState
{
    public EnemyNormalState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }

    public override void EnterState()
    {
        BaseEnemy.Target = GameManager.Instance.PlayerTrm;
    }
    
    public override void UpdateState()
    {
        float range = 3f;
        Vector3 originPos = _owner.transform.position;
        int layer = 1 << LayerMask.NameToLayer("Player"); 
        Collider[] cols = Physics.OverlapSphere(originPos,range,layer);
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                _stateMachine.ChangeState(EEnemyState.Chase);
            }
        }
    }
}
