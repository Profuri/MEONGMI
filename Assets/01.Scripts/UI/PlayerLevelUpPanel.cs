using DG.Tweening;
using System;
using UnityEngine;

public class PlayerLevelUpPanel : UIComponent
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
}