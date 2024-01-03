using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : MonoSingleton<ResManager>
{
    [SerializeField] private BaseStatSO _baseStatSO;
    private ResSpawner _resSpawner;

    //public int PlayerResourceCnt { get; private set; }
    //public int BaseResourceCnt { get; private set; }
    
    private int playerResourceCnt;
    public int PlayerResourceCnt
    {
        get { return playerResourceCnt; }
        private set 
        {
            if (value != playerResourceCnt)
            {
                playerResourceCnt = Mathf.Clamp(playerResourceCnt, 0, StatManager.Instance.GetPlayerResMax()); 
                TestUIManager.Instance.IngameUI.UpdatePlayerResource();
            }
        }
    }

    private int baseResourceCnt;
    public int BaseResourceCnt
    {
        get { return baseResourceCnt; }
        private set 
        {
            if (value != playerResourceCnt)
            {
                baseResourceCnt = Mathf.Clamp(baseResourceCnt, 0, StatManager.Instance.MaxBaseResValue);
                TestUIManager.Instance.IngameUI.UpdateBaseResource();
            }
        }
    }

    public event Action OnResourceToZero;

    public override void Init()
    {
        _resSpawner = new ResSpawner(PhaseManager.Instance.PhaseInfoList);
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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            PlayerResourceCnt += 10000;
    }
}
