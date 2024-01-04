using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PhaseLogPanel : UIComponent
{
    [SerializeField] private RectTransform _upperBound;
    [SerializeField] private RectTransform _downBound;

    [SerializeField] private RectTransform _mainTextPanel;
    [SerializeField] private RectTransform _upperTextPanel;
    [SerializeField] private RectTransform _downTextPanel;

    private TextMeshProUGUI _mainText;
    private TextMeshProUGUI _upperText;
    private TextMeshProUGUI _downText;

    public override void GenerateUI(Transform parent)
    {
        base.GenerateUI(parent);
        Initialize();
    }

    protected override void GenerateTransition()
    {
        _upperBound.DOAnchorPos(new Vector2(0, 0), 0.5f);
        _downBound.DOAnchorPos(new Vector2(0, 0), 0.5f);

        _mainTextPanel.DOScaleY(1, 0.25f);
        _upperTextPanel.DOScaleY(1, 0.25f);
        _downTextPanel.DOScaleY(1, 0.25f);
    }

    protected override void RemoveTransition(Action callback)
    {
        _upperBound.DOAnchorPos(new Vector2(0, 150), 0.5f);
        _downBound.DOAnchorPos(new Vector2(0, -150), 0.5f);

        _mainTextPanel.DOScaleY(0, 0.25f);
        _upperTextPanel.DOScaleY(0, 0.25f);
        _downTextPanel.DOScaleY(0, 0.25f).OnComplete(() => callback?.Invoke());
    }

    private void Initialize()
    {
        
    }
}