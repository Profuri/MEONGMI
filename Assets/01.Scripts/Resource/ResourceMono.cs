using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class ResourceMono : Orb
{
    private int _life;
    
    private int _curResCnt;
    private bool _isOn = false;
    private MeshRenderer _meshRenderer;

    private Coroutine _dissolveCoroutine;

    private PlayerController _playerController;
    
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

        if (_playerController is null)
        {
            _playerController = entity as PlayerController;
            _playerController.OnHammerDownEvent += Gather;
        }
    }

    private void Remove()
    {
        _playerController.OnHammerDownEvent -= Gather;
        _playerController = null;
        if (_isOn)
        {
            GetResource();
            StartDissolveCor(0f,1f,0.7f,() => PoolManager.Instance.Push(this));
        }
    }

    public override void Init()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        StartDissolveCor(1f,0f,3f,() => _isOn = true);
    }

    private void Gather()
    {
        var particle = PoolManager.Instance.Pop("GatheringParticle") as PoolableParticle;
        particle.SetPositionAndRotation(transform.position);
        particle.Play();
        
        _life--;

        if (_life <= 0)
        {
            Remove();
        }
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
        _life = (int)(cnt / 100f);
    }

    public void SetScale(float scale)
    {
        if (scale < 1f)
        {
            scale = 1f;
        }
        
        transform.localScale = new Vector3(scale, scale, scale);
        _interactRadius = scale + 1f;
    }
}
