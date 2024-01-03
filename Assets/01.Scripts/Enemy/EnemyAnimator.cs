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
    protected readonly int _blinkHash = Shader.PropertyToID("_Blink");

    
    protected List<Renderer> _meshRendererList = new List<Renderer>();
    
    private Coroutine _dissolveCoroutine;
    private Coroutine _blinkCoroutine;
    
    public void Init(BaseEnemy baseEnemy, Animator animator)
    {
        _baseEnemy = baseEnemy;
        _animator = animator;
        
         GetComponentsInChildren(_meshRendererList);
         
         foreach (Renderer meshRenderer in _meshRendererList)
         {
             MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
             meshRenderer.GetPropertyBlock(matPropBlock);
             matPropBlock.SetFloat(_dissolveHash,0f);
             matPropBlock.SetFloat(_blinkHash,0f);
             meshRenderer.SetPropertyBlock(matPropBlock);
         }
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
        _dissolveCoroutine = StartCoroutine(FloatCoroutine(startValue, endValue, time, Callback,_dissolveHash));
    }

    public void StartBlinkCoroutine(float startValue, float endValue, float time = 0.1f, Action Callback = null)
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
        }

        _blinkCoroutine = StartCoroutine(BlinkCoroutine(startValue, endValue, time, Callback, _blinkHash));
    }

    private IEnumerator BlinkCoroutine(float startValue, float endValue, float time, Action Callback, int hash)
    {
        foreach (Renderer meshRenderer in _meshRendererList)
        {
            MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(matPropBlock);
            matPropBlock.SetFloat(hash,endValue);
            meshRenderer.SetPropertyBlock(matPropBlock);
        }

        yield return new WaitForSeconds(time);
        
        foreach (Renderer meshRenderer in _meshRendererList)
        {
            MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
            meshRenderer.GetPropertyBlock(matPropBlock);
            matPropBlock.SetFloat(hash,startValue);
            meshRenderer.SetPropertyBlock(matPropBlock);
        }

        Callback?.Invoke();
    }
    private IEnumerator FloatCoroutine(float startValue, float endValue, float time , Action Callback,int hash)
    {
        float timer = 0f;

        while (timer <= time)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(startValue, endValue, timer / time);
            
            foreach (Renderer meshRenderer in _meshRendererList)
            {
                MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
                meshRenderer.GetPropertyBlock(matPropBlock);
                matPropBlock.SetFloat(hash,value);
                meshRenderer.SetPropertyBlock(matPropBlock);
            }
            yield return null;
        }
        Callback?.Invoke();
    }
}
