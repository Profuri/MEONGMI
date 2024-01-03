using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseUnit : Entity
{
    protected UnitType _unitType;
    public UnitType UnitType => _unitType;

    protected UnitLineConnect _lineConnect;
    public UnitLineConnect LineConnect => _lineConnect;

    protected Line _line;
    public Line Line => _line;

    public UnitStatSO UnitStatSO => _entityStatSO as UnitStatSO;

    protected NavMeshAgent _navMesh;
    public NavMeshAgent NavMesh => _navMesh;

    public override void Awake()
    {
        base.Awake();
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _lineConnect = GetComponent<UnitLineConnect>();
        transform.position = Vector3.zero;
        _navMesh.enabled = true;
    }

    public void SetLine(Line line, Transform baseConnectHole)
    {
        _line = line;
        _lineConnect.Init(line, baseConnectHole);
    }

    public override void Init()
    {

    }
}
