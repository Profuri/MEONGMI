using System;
using System.Collections;
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

    [SerializeField] private float _logTime = 2f;

    private TextMeshProUGUI _mainText;
    private TextMeshProUGUI _upperText;
    private TextMeshProUGUI _downText;

    private void Awake()
    {
        _mainText = _mainTextPanel.GetComponentInChildren<TextMeshProUGUI>();
        _upperText = _upperTextPanel.GetComponentInChildren<TextMeshProUGUI>();
        _downText = _downTextPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void GenerateUI(Transform parent)
    {
        Initialize();
        base.GenerateUI(parent);
    }

    protected override void GenerateTransition()
    {
        _upperBound.DOKill();
        _downBound.DOKill();
        _mainTextPanel.DOKill();
        _upperTextPanel.DOKill();
        _downTextPanel.DOKill();
        
        _upperBound.DOAnchorPos(new Vector2(0, 0), 0.5f);
        _downBound.DOAnchorPos(new Vector2(0, 0), 0.5f);

        _mainTextPanel.DOScaleY(1, 0.25f);
        _upperTextPanel.DOScaleY(1, 0.25f);
        _downTextPanel.DOScaleY(1, 0.25f);
    }

    private IEnumerator LogRoutine()
    {
        yield return new WaitForSeconds(_logTime);
        UIManager.Instance.ChangeUI("InGameHUD");
    }

    protected override void RemoveTransition(Action callback)
    {
        _upperBound.DOKill();
        _downBound.DOKill();
        _mainTextPanel.DOKill();
        _upperTextPanel.DOKill();
        _downTextPanel.DOKill();
        
        _upperBound.DOAnchorPos(new Vector2(0, 150), 0.5f);
        _downBound.DOAnchorPos(new Vector2(0, -150), 0.5f);

        _mainTextPanel.DOScaleY(0, 0.25f);
        _upperTextPanel.DOScaleY(0, 0.25f);
        _downTextPanel.DOScaleY(0, 0.25f).OnComplete(() => callback?.Invoke());
    }

    private void Initialize()
    {
        if (PhaseManager.Instance.PhaseType == PhaseType.Raid)
        {
            _mainText.text = $"PHASE  {PhaseManager.Instance.Phase}";
            _upperText.text = "WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING";
            _downText.text = "WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING WARNING";
        }
        else
        {
            _mainText.text = "REST TIME";
            _upperText.text = "SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE";
            _downText.text = "SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE SAFE";
        }
        GameManager.Instance.StartCoroutine(LogRoutine());
    }
}