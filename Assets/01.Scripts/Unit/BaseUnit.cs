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

    protected Transform _target;
    public Transform Target => _target;

    protected Vector3 _holdPosition;
    public Vector3 HoldPosition { get => _holdPosition; set => _holdPosition = value; }

    public override void Awake()
    {
        base.Awake();
        AnimatorCompo = transform.Find("Visual").GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _lineConnect = GetComponent<UnitLineConnect>();
        transform.position = Vector3.zero;
        _navMesh.enabled = true;
        _navMesh.speed = UnitStatSO.moveSpeed;
    }

    public override void Init()
    {
        _navMesh.enabled = true;
    }

    public void SetLine(Line line)
    {
        LineConnect.SetLine(line);
    }

    public void SetPosition(Vector3 pos)
    {
        NavMesh.enabled = false;
        transform.position = pos;
        NavMesh.enabled = true;
    }

    public void SetTarget(Transform trm)
    {
        _target = trm;
    }

    public void StopImmediately()
    {
        if (NavMesh.enabled)
        {
            NavMesh.SetDestination(transform.position);
        }
    }
}
