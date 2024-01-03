using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ResourceMono : PoolableMono
{
    private int _curResCnt;

    public void GetResource()
    {
        if (ResManager.Instance.AddResource(_curResCnt) == false)
        {
            Debug.Log("Can't add more resource!! ");
        }

        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        
    }

    public void SetResourceCnt(int cnt)
    {
        _curResCnt = cnt;
    }
}
