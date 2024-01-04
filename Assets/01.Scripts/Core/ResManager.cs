using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoSingleton<ResManager>
{
    [SerializeField] private BaseStatSO _baseStatSO;
    private ResSpawner _resSpawner;

    public int PlayerResourceCnt { get; private set; }
    public int BaseResourceCnt { get; private set; }
    
    public event Action OnResourceToZero;

    public override void Init()
    {
        _resSpawner = new ResSpawner(PhaseManager.Instance.PhaseInfoList);
    }

    public bool CanUseResource(int resourceCnt) => BaseResourceCnt >= resourceCnt;

    public bool AddResource(int plusResourceCnt)
    {
        int curCnt = PlayerResourceCnt + plusResourceCnt;
        PlayerResourceCnt = curCnt;
        PlayerResourceCnt = Mathf.Clamp(PlayerResourceCnt, 0, _baseStatSO.MaxResCnt);
        
        Debug.Log($"CurrentResourceCnt: {PlayerResourceCnt}");
        //TestUIManager.Instance.IngameUI.UpdatePlayerResource();
        return curCnt <= _baseStatSO.MaxResCnt;
    }

    public bool UseResource(int resourceCnt)
    {
        if (CanUseResource(resourceCnt))
        {
            BaseResourceCnt -= resourceCnt;
            if (BaseResourceCnt == 0)
            {
                OnResourceToZero?.Invoke();
            }
            //TestUIManager.Instance.IngameUI.UpdateBaseResource();
            return true;
        }
        return false;
    }

    public void MoveResource()
    {
        int baseMaxValue = StatManager.Instance.MaxBaseResValue;
        if (PlayerResourceCnt + BaseResourceCnt > baseMaxValue)
        {
            int remainMoney = PlayerResourceCnt - (baseMaxValue - BaseResourceCnt);
            PlayerResourceCnt = remainMoney;
            BaseResourceCnt = baseMaxValue;
        }
        else
        {
            BaseResourceCnt += PlayerResourceCnt;
            PlayerResourceCnt = 0;
        }
        //TestUIManager.Instance.IngameUI.UpdateBaseResource();
        //TestUIManager.Instance.IngameUI.UpdatePlayerResource();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            PlayerResourceCnt += 1;
        if (Input.GetKeyDown(KeyCode.G))
            BaseResourceCnt += 1;
    }
}
