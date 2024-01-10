using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResManager : MonoSingleton<ResManager>
{
    [SerializeField] private BaseStatSO _baseStatSO;
    private ResSpawner _resSpawner;

    private int _playerResCnt;
    public int PlayerResCnt => _playerResCnt;
    private int _baseResCnt = 10000;
    public int BaseResCnt => _baseResCnt;
    
    public Action OnResourceToZero;

    public event Action<int> OnChangePlayerRes;
    public event Action<int> OnChangeBaseRes;

    public override void Init()
    {
        _resSpawner = new ResSpawner(PhaseManager.Instance.PhaseInfoList);
        _baseResCnt = 0;
    }

    public bool CanUseResource(int resourceCnt) => _baseResCnt >= resourceCnt;

    public bool AddResource(int plusResourceCnt)
    {
        int curCnt = _playerResCnt + plusResourceCnt;
        _playerResCnt = curCnt;
        _playerResCnt = Mathf.Clamp(_playerResCnt, 0, _baseStatSO.MaxResCnt);
        
        OnChangePlayerRes?.Invoke(_playerResCnt);
        return curCnt <= _baseStatSO.MaxResCnt;
    }

    public bool UseResource(int resourceCnt)
    {
        if (CanUseResource(resourceCnt))
        {
            _baseResCnt -= resourceCnt;
            if (_baseResCnt == 0)
            {
                OnResourceToZero?.Invoke();
            }

            OnChangeBaseRes?.Invoke(_baseResCnt);
            return true;
        }
        return false;
    }

    public void MinusResource(int minusResourceCnt)
    {
        _baseResCnt -= minusResourceCnt;
        _baseResCnt = Mathf.Clamp(_baseResCnt, 0, _baseStatSO.MaxResCnt);
        OnChangeBaseRes?.Invoke(_baseResCnt);
        if (_baseResCnt == 0)
        {
            OnResourceToZero?.Invoke();
        }
    }

    public void MoveResource()
    {
        int baseMaxValue = StatManager.Instance.MaxBaseResValue;
        if (_playerResCnt + _baseResCnt > baseMaxValue)
        {
            int remainMoney = _playerResCnt - (baseMaxValue - _baseResCnt);
            _playerResCnt = remainMoney;
            _baseResCnt = baseMaxValue;
        }
        else
        {
            _baseResCnt += _playerResCnt;
            _playerResCnt = 0;
        }
        OnChangePlayerRes?.Invoke(_playerResCnt);
        OnChangeBaseRes?.Invoke(_baseResCnt);
    }

    public void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            _baseResCnt += 1000;
            
            OnChangePlayerRes?.Invoke(_playerResCnt);
            OnChangeBaseRes?.Invoke(_baseResCnt);
        }
    }
}
