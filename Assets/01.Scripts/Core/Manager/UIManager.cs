using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private Canvas _mainCanvas;

    private Stack<UIComponent> _componentStack;
    public UIComponent TopComponent => _componentStack.Peek();
    public bool ComponentStackEmpty => _componentStack.Count <= 0;
    
    public override void Init()
    {
        _componentStack = new Stack<UIComponent>();
        // GenerateUI("");
    }

    public UIComponent GenerateUI(string componentName, UIGenerateOption options = UIGenerateOption.Stackable | UIGenerateOption.ClearPanel | UIGenerateOption.ResettingPos, Transform parent = null)
    {
        if (parent is null)
        {
            parent = _mainCanvas.transform;
        }

        var ui = PoolManager.Instance.Pop(componentName) as UIComponent;

        if (ui is null)
        {
            return null;
        }
        
        ui.GenerateUI(parent, options);

        if (options.HasFlag(UIGenerateOption.Stackable))
        {
            _componentStack.Push(ui);
        }

        return ui;
    }

    public void RemoveTopUI()
    {
        var top = _componentStack.Pop();
        top.RemoveUI();
    }

    public void ReturnUI()
    {
        if (ComponentStackEmpty)
        {
            Debug.LogWarning("There is not exist current UI");
            return;
        }

        var cur = _componentStack.Pop();

        if (ComponentStackEmpty)
        {
            Debug.LogWarning("There is not exist prev UI");
            return;
        }

        var prev = _componentStack.Pop();
        
        cur.RemoveUI();
        GenerateUI(prev.name, prev.Options, prev.Parent);
    }

    public void ClearPanel()
    {
        var uis = new List<UIComponent>();
        _mainCanvas.GetComponentsInChildren(uis);

        foreach (var ui in uis)
        {
            ui.RemoveUI();
        }
    }
}