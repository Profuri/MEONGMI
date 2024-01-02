using System.Collections;
using UnityEngine;

public class PoolableParticle : PoolableMono
{
    private ParticleSystem _particleSystem;
    
    public override void Init()
    {
        if (_particleSystem is null)
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
    }

    public void SetPositionAndRotation(Vector3 pos = default, Quaternion rot = default)
    {
        _particleSystem.transform.SetPositionAndRotation(pos, rot);
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        _particleSystem.Play();
        yield return new WaitUntil(() => !_particleSystem.isPlaying);
        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }
}