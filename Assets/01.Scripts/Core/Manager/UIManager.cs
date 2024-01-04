using System;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _mainCanvas;
    
    public UIComponent CurrentComponent { get; private set; }

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

        if (CurrentComponent is null)
        {
            ui.GenerateUI(parent);
        }
        else
        {
            CurrentComponent.RemoveUI(() =>
            {
                ui.GenerateUI(parent);
                callback?.Invoke();
            });
        }

        CurrentComponent = ui;
    }

    public override void Init()
    {
        ChangeUI("InGameHUD");
    }
}