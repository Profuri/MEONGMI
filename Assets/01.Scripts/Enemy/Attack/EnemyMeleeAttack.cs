using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    protected override void Attack()
    {
        base.Attack();

        Vector3 lookPos = _baseEnemy.Target.position - _baseEnemy.transform.position;
        lookPos.y = 0f;
        _baseEnemy.transform.rotation = Quaternion.LookRotation(lookPos);
        
        Debug.Log("Enemy Melee Attack");
        
        Vector3 originPos = _baseEnemy.transform.position;
        float radius = _baseEnemy.EnemyAttackSO.attackRange;
        int layer = _baseEnemy.LayerMask;

        Collider[] cols = Physics.OverlapSphere(originPos,radius,layer);
        
        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damaged(DamageType.None, _baseEnemy.EnemyAttackSO.damage);
                    break;
                }
            }
        }
        _lastAtkTime = Time.time;
    }
}
