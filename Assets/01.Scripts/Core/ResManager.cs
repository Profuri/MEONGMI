using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoSingleton<ResManager>
{
    [SerializeField] private BaseStatSO _baseStatSO;

    public int PlayerResourceCnt { get; private set; }
    public int BaseResourceCnt { get; private set; }
    public event Action OnResourceToZero;

    public override void Init()
    {

    }

    public bool CanUseResource(int resourceCnt) => PlayerResourceCnt >= resourceCnt;

    public bool AddResource(int plusResourceCnt)
    {
        int curCnt = PlayerResourceCnt + plusResourceCnt;
        PlayerResourceCnt = curCnt;
        PlayerResourceCnt = Mathf.Clamp(PlayerResourceCnt, 0, _baseStatSO.MaxResCnt);

        Debug.Log($"CurrentResourceCnt: {PlayerResourceCnt}");
        return curCnt <= _baseStatSO.MaxResCnt;
    }

    public bool UseResource(int resourceCnt)
    {
        if (CanUseResource(resourceCnt))
        {
            PlayerResourceCnt -= resourceCnt;
            if (PlayerResourceCnt == 0)
            {
                OnResourceToZero?.Invoke();
            }
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            PlayerResourceCnt += 10000;
    }
}
