using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseUnit : Entity
{
    protected UnitType _unitType;
    public UnitType UnitType => _unitType;

    protected UnitLineConnect _unitLine;
    public UnitLineConnect UnitLine => _unitLine;

    protected UnitStatSO _unitStatSO;
    public UnitStatSO UnitStatSO => _unitStatSO;

    protected NavMeshAgent _navMesh;
    public NavMeshAgent NavMesh => _navMesh;

    public override void Awake()
    {
        base.Awake();
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _unitLine = GetComponent<UnitLineConnect>();
    }
}
