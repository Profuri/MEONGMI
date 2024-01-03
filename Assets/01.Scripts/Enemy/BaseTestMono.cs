using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BaseTestMono : MonoBehaviour
{
    private void Awake()
    {
        _hp = _entityStatSO.maxHp;
    }
    public void Damaged(float damage)
    {
        VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        Vector3 offset = Vector3.up * 1.5f;
        vfxPlayer.transform.position = transform.position + offset;
        vfxPlayer.PlayEffect();
        
        _hp -= damage;
        
        CameraManager.Instance.ImpulseCam(1, 0.1f, new Vector3(0, -1, 0));
    }
}
