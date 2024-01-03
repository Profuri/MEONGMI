using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseForTest : MonoBehaviour, IDamageable
{
    [SerializeField] private EntityStatSO _entityStatSO;
    private float _hp;

    private void Awake()
    {
        _hp = _entityStatSO.maxHp;
    }

    public void CreateUnit(UnitType type)
    {
        BaseUnit unit = UnitManager.Instance.CreateUnit(type);
        unit.transform.SetParent(transform);
        unit.SetLine(CreateLine(), transform);
    }

    private Line CreateLine()
    {
        Line line = PoolManager.Instance.Pop("Line") as Line;
        return line;
    }

    public void Damaged(float damage)
    {
        VFXPlayer vfxPlayer = PoolManager.Instance.Pop("ResHitParticle") as VFXPlayer;
        Vector3 offset = Vector3.up * 1.5f;
        vfxPlayer.transform.position = transform.position + offset;
        vfxPlayer.PlayEffect();

        _hp -= damage;
    }

    //Debug
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateUnit(UnitType.Attacker);
        }
    }
}