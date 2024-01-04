using System;
using DG.Tweening;
using UnityEngine;

public class BaseUpgradePanel : UIComponent
{
    public override void GenerateUI(Transform parent)
    {
        base.GenerateUI(parent);
        GenerateTransition();
    }

    protected override void GenerateTransition()
    {
        ((RectTransform)transform).DOScaleY(1, 0.5f);
    }

    protected override void RemoveTransition(Action callback)
    {
        ((RectTransform)transform).DOScaleY(0, 0.5f).OnComplete(() =>
        {
            base.RemoveUI(null);
            callback?.Invoke();
        });
    }
    
    public void UpgradeBase()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    }
    public void UpgradePlayer()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    }
    public void UpgradeTrait()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.TRAIT);
    }
}