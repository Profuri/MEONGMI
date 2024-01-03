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
    public event Action OnDeadEvent;
    
    protected readonly int _hitHash = Animator.StringToHash("HIT");
    protected readonly int _attackHash = Animator.StringToHash("ATTACK");
    protected readonly int _deadHash = Animator.StringToHash("DEAD");
    
    protected readonly int _dissolveHash = Shader.PropertyToID("_Dissolve");

    
    protected List<SkinnedMeshRenderer> _meshRendererList = new List<SkinnedMeshRenderer>();
    
    private Coroutine _dissolveCoroutine;
    
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

    public void OnDead()
    {
        OnDeadEvent?.Invoke();
        _baseEnemy.AnimatorCompo.ResetTrigger(_deadHash);
    }
    
    public void StartDissolveCor(float startValue, float endValue, float time = 0.5f, Action Callback = null)
    {
        _dissolveCoroutine = StartCoroutine(DissolveCoroutine(startValue, endValue, time, Callback));
    }
    private IEnumerator DissolveCoroutine(float startValue, float endValue, float time , Action Callback)
    {
        float timer = 0f;

        float value = Mathf.Clamp(startValue, endValue, timer / time);
        while (timer <= time)
        {
            timer += Time.deltaTime;

            foreach (SkinnedMeshRenderer meshRenderer in _meshRendererList)
            {
                MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
                meshRenderer.GetPropertyBlock(matPropBlock);
                matPropBlock.SetFloat(_dissolveHash,value);
                meshRenderer.SetPropertyBlock(matPropBlock);
            }
            yield return null;
        }
        Callback?.Invoke();
    }
}
