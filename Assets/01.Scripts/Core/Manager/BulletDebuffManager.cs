using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDebuffManager : MonoSingleton<BulletDebuffManager>
{
    [SerializeField] BulletDebuffSO debuffSO;

    [HideInInspector] public float SlowPercent;
    [HideInInspector] public float SlowDuration;
    [HideInInspector] public int PoisonTickCount;
    [HideInInspector] public float PoisonApplyDuration;
    [HideInInspector] public float TickDamagePercent;
    public override void Init()
    {
        if(debuffSO != null)
        {
            SlowPercent = debuffSO.SlowPercent;
            SlowDuration = debuffSO.SlowDuration;
            PoisonTickCount = debuffSO.PoisonTickCount;
            PoisonApplyDuration = debuffSO.PoisonApplyDuration;
            TickDamagePercent = debuffSO.TickDamagePercent;
        }
    }
}
