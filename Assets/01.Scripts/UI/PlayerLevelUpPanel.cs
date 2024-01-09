using DG.Tweening;
using System;
using InputControl;
using UnityEngine;

public class PlayerLevelUpPanel : UIComponent
{
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
        ((RectTransform)transform).DOKill();
        ((RectTransform)transform).DOScaleY(1, 0.5f);
    }

    protected override void RemoveTransition(Action callback)
    {
        ((RectTransform)transform).DOKill();
        ((RectTransform)transform).DOScaleY(0, 0.5f).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
}