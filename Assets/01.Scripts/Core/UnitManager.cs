using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoSingleton<UnitManager>
{
    [SerializeField] private BaseStatSO _baseData;

    private List<BaseUnit> _unitList;

    public override void Init()
    {
        _unitList = new List<BaseUnit>();
    }

    public BaseUnit CreateUnit(UnitType type)
    {
        BaseUnit unit = PoolManager.Instance.Pop($"{type.ToString()}Unit") as BaseUnit;
        return unit;
    }
}
