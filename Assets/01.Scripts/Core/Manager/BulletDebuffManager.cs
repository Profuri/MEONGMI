using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDebuffManager : MonoSingleton<BulletDebuffManager>
{
    [SerializeField] BulletDebuffSO debuffSO;

    public float SlowPercent => debuffSO.SlowPercent;
    public float SlowDuration => debuffSO.SlowDuration;
    public int PoisonTickCount => debuffSO.PoisonTickCount;
    public float PoisonApplyDuration => debuffSO.PoisonApplyDuration;
    public float TickDamagePercent => debuffSO.TickDamagePercent;
    
    public override void Init()
    {
     
    }
}
