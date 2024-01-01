using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalState : EnemyState
{
    public EnemyNormalState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }

    public override void UpdateState()
    {
        float range = 3f;
        Vector3 originPos = _owner.transform.position;
        Collider[] cols = Physics.OverlapSphere(originPos,range);
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out ResourceMono resMono))
                {
                    resMono.GetResource();
                }
            }
        }
    }
}
