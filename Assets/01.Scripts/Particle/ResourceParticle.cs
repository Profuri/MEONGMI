using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceParticle : PoolableMono
{
    private ParticleSystem _particleSystem;
    public override void Init()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.loop = true;
    }
    public void ChaseLine(LineRenderer line)
    {
        StartCoroutine(ChaseLineCor(line));
    }

    private IEnumerator ChaseLineCor(LineRenderer line)
    {
        _particleSystem.Play();
        int curIdx = line.positionCount - 1;
        Vector3[] positions = new Vector3[line.positionCount];
        line.GetPositions(positions);
        while (curIdx > 0)
        {
            transform.position = positions[curIdx];
            curIdx--;
            yield return null;
        }

        _particleSystem.Stop();
        PoolManager.Instance.Push(this);
    }
}
