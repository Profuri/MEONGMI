using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Collider))]
public class ResourceMono : Orb
{
    private int _curResCnt;
    private bool _isOn = false;
    public bool IsInteractive = false;
    public bool Invalid = false;
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
    }

    public override void OnInteract(Entity entity)
    {
        base.OnInteract(entity);
        IsInteractive = true;
        PlayerController player = entity as PlayerController;
        if(player != null)
        {
            player.OnHammerDownEvent += Remove;
        }

        MinerUnit miner = entity as MinerUnit;
        if (miner != null)
        {
            miner.EndGather += Remove;
        }
    }

    private void Remove(Entity entity)
    {
        PlayerController player = entity as PlayerController;
        if (player != null)
        {
            player.OnHammerDownEvent -= Remove;
        }

        MinerUnit miner = entity as MinerUnit;
        if (miner != null)
        {
            miner.EndGather -= Remove;
        }

        if (_isOn)
        {
            GetResource();
            StartDissolveCor(0f,1f,0.7f,() => PoolManager.Instance.Push(this));
        }
    }

    public override void Init()
    {
        base.Init();
        IsInteractive = false;
        Invalid = false;
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

    public void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, scale);
        _interactRadius = scale + 1f;
    }
}
