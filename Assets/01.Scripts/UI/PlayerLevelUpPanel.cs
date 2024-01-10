using DG.Tweening;
using System;
using InputControl;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelUpPanel : UIComponent
{
    [SerializeField] private Image _bg;
    [SerializeField] private RectTransform _baseTrm;
    
    [SerializeField] private GameObject _cardTemplete;
    [SerializeField] private UpgradeContainer _upgradeContainer;
    
    public override void GenerateUI()
    {
        base.GenerateUI();
        UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    }

    public void AddElem(int elem)
    {
        _upgradeContainer.SetUpgrade(_cardTemplete, EUpgradeType.PLAYER, elem);
    }

    protected override void GenerateTransition()
    {
        _bg.DOKill();
        _baseTrm.DOKill();
        _bg.DOFade(0.5f, 0.5f);
        _baseTrm.DOScaleY(1, 0.5f);
    }

    protected override void RemoveTransition(Action callback)
    {
        _bg.DOKill();
        _baseTrm.DOKill();
        _bg.DOFade(0, 0.5f);
        _baseTrm.DOScaleY(0, 0.5f).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
}