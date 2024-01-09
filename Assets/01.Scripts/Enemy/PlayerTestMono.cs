using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTestMono : MonoBehaviour,IDetectable,IDamageable
{
    [SerializeField] private EntityStatSO _entityStatSO;

    private float _hp;

    private void Awake()
    {
        _hp = _entityStatSO.maxHp;
    }
    public Transform Detect()
    {
        return this.transform;
    }

    public void Damaged(DamageType type, float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Debug.Log("OnDead");
            Destroy(this.gameObject);
        }
    }
}
