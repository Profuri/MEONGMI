using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] private Transform _muzzle;
    
    public override void AttackHandle()
    {
        base.AttackHandle();
        Vector3 lookPos = _baseEnemy.Target.position - _baseEnemy.transform.position;
        lookPos.y = 0f;
        _baseEnemy.transform.rotation = Quaternion.LookRotation(lookPos);
    }
    protected override void Attack()
    {
        base.Attack();
        
        
        
    }
}
