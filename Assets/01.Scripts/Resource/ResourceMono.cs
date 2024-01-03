using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class ResourceMono : PoolableMono
{
    private int _curResCnt;
    private bool _isOn = false;
    private MeshRenderer _meshRenderer;

    private Coroutine _dissolveCoroutine;
    
    private readonly int _dissolveHash = Shader.PropertyToID("_Dissolve");
    public void GetResource()
    {
        if (_isOn == false) return;
        if (ResManager.Instance.AddResource(_curResCnt) == false)
        {
            Debug.Log("Can't add more resource!! ");
        }

        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        StartDissolveCor(1f,0f,3f,() => _isOn = true);
    }
    public void StartDissolveCor(float startValue, float endValue, float time = 0.5f, Action Callback = null)
    {
        _dissolveCoroutine = StartCoroutine(DissolveCoroutine(startValue, endValue, time, Callback));
    }
    private IEnumerator DissolveCoroutine(float startValue, float endValue, float time , Action Callback)
    {
        float timer = 0f;

        while (timer <= time)
        {
            timer += Time.deltaTime;
            float value = Mathf.Lerp(startValue, endValue, timer / time);
            
            MaterialPropertyBlock matPropBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(matPropBlock);
            matPropBlock.SetFloat(_dissolveHash,value);
            _meshRenderer.SetPropertyBlock(matPropBlock);
            yield return null;
        }
        Callback?.Invoke();
    }
    public void SetResourceCnt(int cnt)
    {
        _curResCnt = cnt;
    }
}
