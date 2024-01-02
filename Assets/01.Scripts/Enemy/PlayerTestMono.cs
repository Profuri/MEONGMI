using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTestMono : MonoBehaviour,IDetectable,IDamageable
{
    public Transform Detect()
    {
        return this.transform;
    }

    public void Damaged(int damage)
    {
        Debug.Log($"Damage: {damage}");
    }
}
