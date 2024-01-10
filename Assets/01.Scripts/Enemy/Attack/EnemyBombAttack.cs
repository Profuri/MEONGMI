using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombAttack : EnemyAttack
{
    protected override void Attack()
    {
        base.Attack();
        
        Vector3 lookPos = _baseEnemy.Target.position - _baseEnemy.transform.position;
        lookPos.y = 0f;
        _baseEnemy.transform.rotation = Quaternion.LookRotation(lookPos);
        
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
                    VFXPlayer bombEffect = PoolManager.Instance.Pop("BombEnemyParticle") as VFXPlayer;
                    bombEffect.transform.position = transform.position;
                    bombEffect.PlayEffect();
                    damageable.Damaged(DamageType.HandleByAttacker, _baseEnemy.EnemyAttackSO.damage);
                }
            }
        }
        _lastAtkTime = Time.time;
        _baseEnemy.NavMeshAgent.enabled = false;
        _baseEnemy.ActionData.IsStopped = true;
        EnemySpawner.Instance.DeadEnemy(_baseEnemy);
    }
}
