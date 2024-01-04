using System;
using DG.Tweening;
using UnityEngine;

public class UIComponent : PoolableMono
{
    private Transform _prevParent;
    public Transform Parent { get; private set; }
    public bool Active { get; private set; }

    public virtual void GenerateUI(Transform parent)
    {
        _prevParent = transform.parent;

        transform.SetParent(parent);
        Parent = parent;

        ((RectTransform)transform).offsetMin = Vector2.zero;
        ((RectTransform)transform).offsetMax = Vector2.zero;
        
        GenerateTransition();
        Active = true;
    }

    public virtual void RemoveUI(Action callback)
    {
        RemoveTransition(() =>
        {
            Active = false;
            transform.SetParent(_prevParent);
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