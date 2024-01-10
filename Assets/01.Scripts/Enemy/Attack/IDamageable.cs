using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    None, // ���� �ʿ� ���� ��.
    HandleByAttacker, //���� ��ü���� ó��.
    HandleByReciver, //���� �޴� ��ü���� ó��.
}

public interface IDamageable
{
    //public void Damaged(DamageType type, float damage);
    public void Damaged(DamageType type, float damage);
}
