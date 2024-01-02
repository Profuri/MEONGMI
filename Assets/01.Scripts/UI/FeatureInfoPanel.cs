using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureInfoPanel : InfoPanel
{
    [SerializeField] private bool _isActive;

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        Color newColor = _icon.color;
        newColor.a = _isActive ? 1f : 0.3f;
        _icon.color = newColor;
    }
#endif
}
