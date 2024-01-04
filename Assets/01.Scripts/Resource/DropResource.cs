using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DropResource : PoolableMono,IGatherable
{
    private int _resourceAmount;

    public override void Init()
    {
        
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
