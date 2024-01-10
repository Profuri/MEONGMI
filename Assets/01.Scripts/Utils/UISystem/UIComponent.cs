using System;
using UnityEngine;

public class UIComponent : PoolableMono
{
    public bool Active { get; private set; }

    public virtual void GenerateUI()
    {
        GenerateTransition();
        Active = true;
    }

    public virtual void RemoveUI(Action callback)
    {
        RemoveTransition(() =>
        {
            Active = false;
            PoolManager.Instance.Push(this);
            callback?.Invoke();
        });
    }

    protected virtual void GenerateTransition()
    {
    }

    protected virtual void RemoveTransition(Action callback)
    {
    }
    
    public override void Init()
    {
    }
}