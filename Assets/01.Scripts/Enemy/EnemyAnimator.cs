using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimator : MonoBehaviour
{
    protected BaseEnemy _baseEnemy;
    protected Animator _animator;

    public event Action OnAttackEndEvent;
    public event Action OnHitEndEvent;
    
    protected readonly int _hitHash = Animator.StringToHash("HIT");
    protected readonly int _attackHash = Animator.StringToHash("ATTACK");
    
    
    public void Init(BaseEnemy baseEnemy, Animator animator)
    {
        _baseEnemy = baseEnemy;
        _animator = animator;
    }

    public void OnHitEnd()
    {
        Debug.Log("OnHitEnd");
        OnHitEndEvent?.Invoke();
        _baseEnemy.StopImmediately(false);
        _baseEnemy.AnimatorCompo.ResetTrigger(_hitHash);
    }

    public void OnAttackEnd()
    {
        OnAttackEndEvent?.Invoke();
        _baseEnemy.AnimatorCompo.ResetTrigger(_attackHash);   
    }
    
}
