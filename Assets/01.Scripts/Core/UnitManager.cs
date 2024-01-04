using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    public List<BaseUnit> UnitList;
    [SerializeField] private UnitType _currentUnit;
    public Dictionary<UnitType, int> UnitCountDictionary;

    public override void Init()
    {
        UnitList = new List<BaseUnit>();
        UnitCountDictionary = new Dictionary<UnitType, int>
        {
            { UnitType.Attacker, 0 },
            { UnitType.Miner, 0 }
        };
    }

    public BaseUnit CreateUnit(UnitType type, Vector3 position)
    {
        BaseUnit unit = PoolManager.Instance.Pop($"{type.ToString()}Unit") as BaseUnit;
        unit.transform.position = position;
        unit.Init();
        return unit;
    }

    public void DeleteUnit(BaseUnit unit)
    {
        unit.LineConnect.Delete();
        PoolManager.Instance.Push(unit);
    }
}
