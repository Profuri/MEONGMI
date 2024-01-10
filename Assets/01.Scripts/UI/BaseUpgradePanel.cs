using System;
using DG.Tweening;
using InputControl;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgradePanel : UIComponent
{
    [SerializeField] private Image _bg;
    [SerializeField] private RectTransform _baseTrm;
    
    [SerializeField] private InputReader _inputReader;
    
    [SerializeField] private GameObject _cardTemplete;
    [SerializeField] private UpgradeContainer _upgradeContainer;
    
    public override void GenerateUI()
    {
        base.GenerateUI();
        _inputReader.OnESCInputEvent += OnESCHandle;
        UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    }

    public override void RemoveUI(Action callback)
    {
        base.RemoveUI(callback);
        _inputReader.OnESCInputEvent -= OnESCHandle;
    }
    
    public void AddElem(int elem)
    {
        _upgradeContainer.SetUpgrade(_cardTemplete, EUpgradeType.BASE, elem);
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
        _bg.DOFade(0f, 0.5f);
        _baseTrm.DOScaleY(0, 0.5f).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }

    private void OnESCHandle()
    {
        UIManager.Instance.ChangeUI("InGameHUD");
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