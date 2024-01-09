using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    None, // 굳이 필요 없을 때.
    HandleByAttacker, //공격 주체에서 처리.
    HandleByReciver, //공격 받는 주체에서 처리.
}

public interface IDamageable
{
    //public void Damaged(DamageType type, float damage);
    public void Damaged(DamageType type, float damage);
}
