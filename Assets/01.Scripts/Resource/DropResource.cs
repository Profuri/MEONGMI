using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DropResource : PoolableMono,IGatherable
{
    [SerializeField] private float _rotateSpeed;
    private int _resourceAmount;

    public override void Init()
    {
        
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotateSpeed);
    }

    public void SetResourceAmount(int resourceAmount)
    {
        _resourceAmount = resourceAmount;
    }

    public int GetGatheringAmount()
    {
        return _resourceAmount;
    }
}
