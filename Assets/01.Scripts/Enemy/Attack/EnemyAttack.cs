using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected BaseEnemy _baseEnemy;
    protected float _lastAtkTime;
    public virtual void Init(BaseEnemy baseEnemy)
    {
        _baseEnemy = baseEnemy;
    }
    public abstract void Attack();

    public virtual bool CanAttack()
    {
        return _lastAtkTime + _baseEnemy.EnemyAttackSO.attackDelay <= Time.time;
    }
}
