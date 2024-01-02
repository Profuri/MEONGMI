using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack()
    {
        base.Attack();
        Debug.Log("Enemy Melee Attack");
        Vector3 originPos = transform.position - transform.forward;
        float radius = _baseEnemy.EnemyAttackSO.attackRange;

        Ray ray = new Ray(originPos,transform.forward);
        bool result = Physics.SphereCast(ray,radius,out RaycastHit hitInfo,_baseEnemy.LayerMask);
        
        if (result)
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damaged(_baseEnemy.EnemyAttackSO.damage);
            }
        }
        _lastAtkTime = Time.time;
    }
}
