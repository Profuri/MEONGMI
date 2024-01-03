using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : Entity
{
    protected UnitType _unitType;
    public UnitType UnitType => _unitType;
}
