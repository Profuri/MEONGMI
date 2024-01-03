using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureInfoPanel : InfoPanel
{
    [SerializeField] ETraitUpgradeElement myType;
    [SerializeField] private bool _isActive;

    public ETraitUpgradeElement GetTraitType => myType;
    public void SetActive(bool value)
    {
        _isActive = value;
        _icon.DOFade(_isActive ? 1f : 0.3f, 0.2f);
    }
    public bool GetActive() => _isActive;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        _icon.DOFade(_isActive ? 1f : 0.3f, 0.2f);
    }
#endif
}
