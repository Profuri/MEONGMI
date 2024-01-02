using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected BaseEnemy _baseEnemy;
    protected float _lastAtkTime;
    protected readonly int _attackHash = Animator.StringToHash("ATTACK");
    
    public virtual void Init(BaseEnemy baseEnemy)
    {
        _baseEnemy = baseEnemy;
    }

    public virtual void AttackHandle()
    {
        _baseEnemy.AnimatorCompo.SetTrigger(_attackHash);
        _baseEnemy.EnemyAnimator.OnAttackEndEvent -= Attack;
        _baseEnemy.EnemyAnimator.OnAttackEndEvent += Attack;
    }

    protected virtual void Attack()
    {
        transform.rotation = Quaternion.LookRotation(_baseEnemy.Target.position);
    }
    public virtual bool CanAttack()
    {
        return _lastAtkTime + _baseEnemy.EnemyAttackSO.attackDelay <= Time.time;
    }
}
