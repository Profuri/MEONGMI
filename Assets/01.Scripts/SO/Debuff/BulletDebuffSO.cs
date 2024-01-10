using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bullet/Debuff")]
public class BulletDebuffSO : ScriptableObject
{
    public float SlowPercent = 0.5f;
    public float SlowDuration = 2f;
    public int PoisonTickCount = 3;
    public float PoisonApplyDuration = 2.4f;
    public float TickDamagePercent = 0.05f; //기본 데미지에 몇퍼 들어가는지.
}
