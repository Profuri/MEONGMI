using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
