using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : PoolableMono
{
    [SerializeField] private float _interval;

    private LineRenderer _lineRenderer;

    private Transform _startHole;
    private Transform _endHole;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public override void Init()
    {
    }

    public void LineUpdate()
    {
        if (_startHole is null || _endHole is null)
        {
            return;
        }

        var distance = Vector3.Distance(_startHole.position, _endHole.position);
        var cnt = Mathf.CeilToInt(distance / _interval);
        _lineRenderer.positionCount = cnt;
        
        var dir = (_endHole.position - _startHole.position).normalized;

        for (var i = 0; i < _lineRenderer.positionCount; i++)
        {
            var pos = _startHole.position + dir * (_interval * i);
            _lineRenderer.SetPosition(i, pos);
        }
    }

    public void SetStartHole(Transform startHole)
    {
        _startHole = startHole;
    }

    public void SetEndHole(Transform endHole)
    {
        _endHole = endHole;
    }
}