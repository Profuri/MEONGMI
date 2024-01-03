using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    private List<BaseUnit> _unitList;

    public int UnitCount => _unitList.Count;

    public override void Init()
    {
        _unitList = new List<BaseUnit>();
    }

    public BaseUnit CreateUnit(UnitType type)
    {
        BaseUnit unit = PoolManager.Instance.Pop($"{type.ToString()}Unit") as BaseUnit;
        unit.transform.position = transform.position;
        return unit;
    }
}