using System;
using System.Collections.Generic;
using InputControl;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private List<UpgradeSelectButton> _upgradeSelectButton;
    [SerializeField] private PausePanel _pausePanel;
    
    public UIComponent CurrentComponent { get; private set; }
    public bool IsTransitioning { get; private set; }

    public void ChangeUI(string componentName, Action callback = null, Transform parent = null)
    {
        if (parent is null)
        {
            parent = _mainCanvas.transform;
        }

        var ui = PoolManager.Instance.Pop(componentName) as UIComponent;

        if (ui is null)
        {
            return;
        }

        IsTransitioning = true;

        if (CurrentComponent is null)
        {
            ui.GenerateUI(parent);
            IsTransitioning = false;
        }
        else
        {
            CurrentComponent.RemoveUI(() =>
            {
                ui.GenerateUI(parent);
                IsTransitioning = false;
                callback?.Invoke();
            });
        }

        CurrentComponent = ui;
    }

    public override void Init()
    {
        ChangeUI("InGameHUD");
    }

    public void Pause()
    {
        if (!_pausePanel.gameObject.activeSelf)
        {
            _pausePanel.Generate();
        }
    }
    
    public void ShowUpgradeUI()
    {
        foreach(var selectBtn in _upgradeSelectButton)
        {
            selectBtn.Show();
        }
    }

    public void HideUpgradeUI()
    {
        foreach (var selectBtn in _upgradeSelectButton)
        {
            selectBtn.HIde();
        }
    }
    
    public void AddUpgradeElem(EUpgradeType upgradeType, int elem)
    {
        switch (upgradeType)
        {
            case EUpgradeType.BASE:
                ((BaseUpgradePanel)CurrentComponent).AddElem(elem);
                break;
            case EUpgradeType.PLAYER:
                ((PlayerLevelUpPanel)CurrentComponent).AddElem(elem);
                break;
        }
    }
}