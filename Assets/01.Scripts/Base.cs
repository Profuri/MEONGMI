using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Base : MonoBehaviour,IDamageable
{
    public Collider Collider { get; private set; }
    private Arc _arc;
    
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        _arc = transform.Find("Arc").GetComponent<Arc>();
        ResManager.Instance.OnResourceToZero += () => Destroy(this.gameObject);
    }
    
    public void Damaged(float damage)
    {
        //이펙트 소환.
        // VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        // Vector3 offset = Vector3.up * 1.5f;
        // vfxPlayer.transform.position = transform.position + offset;
        // vfxPlayer.PlayEffect();
        _arc.ShakePosition();
        ResManager.Instance.UseResource((int)damage);
    }
}
