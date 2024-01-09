using System;
using DG.Tweening;
using InputControl;
using UnityEngine;

public class BaseUpgradePanel : UIComponent
{
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