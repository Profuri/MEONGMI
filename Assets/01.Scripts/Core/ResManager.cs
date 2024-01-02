using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoSingleton<ResManager>
{
    [SerializeField] private BaseStatSO _baseStatSO;

    public int ResourceCnt { get; private set; }
    public event Action OnResourceToZero;

    public override void Init()
    {

    }

    public bool CanUseResource(int resourceCnt) => ResourceCnt >= resourceCnt;

    public bool AddResource(int plusResourceCnt)
    {
        int curCnt = ResourceCnt + plusResourceCnt;
        ResourceCnt = curCnt;
        ResourceCnt = Mathf.Clamp(ResourceCnt, 0, _baseStatSO.MaxResCnt);

        Debug.Log($"CurrentResourceCnt: {ResourceCnt}");
        return curCnt <= _baseStatSO.MaxResCnt;
    }

    public bool UseResource(int resourceCnt)
    {
        if (CanUseResource(resourceCnt))
        {
            ResourceCnt -= resourceCnt;
            if (ResourceCnt == 0)
            {
                OnResourceToZero?.Invoke();
            }
            return true;
        }
        return false;
    }

 
}