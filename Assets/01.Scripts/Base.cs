using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Base : MonoBehaviour,IDamageable
{
    private int curUnitCount;
    public int CurUnitCount
    {
        get { return curUnitCount; }
        private set { Mathf.Clamp(curUnitCount, 0, StatManager.Instance.UnitMaxValue); }
    }

    public Collider Collider { get; private set; }
    private Arc _arc;
    
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        _arc = transform.Find("Arc").GetComponent<Arc>();
        ResManager.Instance.OnResourceToZero += () => Destroy(this.gameObject);
        curUnitCount = 0;
    }
    
    public void Damaged(float damage)
    {
        //이펙트 소환.
        VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        Vector3 offset = Vector3.up * 0.75f;
        vfxPlayer.transform.position = _arc.transform.position + offset;
        vfxPlayer.PlayEffect();
        _arc.ShakePosition();
        
        CameraManager.Instance.ImpulseCam(1, 0.1f, new Vector3(0, -1, 0));
        ResManager.Instance.UseResource((int)damage);
    }
    public void AddUnit()
    {
        curUnitCount++;
    }
}
