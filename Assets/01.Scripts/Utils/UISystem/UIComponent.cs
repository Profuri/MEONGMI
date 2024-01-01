using UnityEngine;

public class UIComponent : PoolableMono
{
    private Transform _prevParent;

    public Transform Parent { get; private set; }
    public UIGenerateOption Options { get; private set; }
    public bool Active { get; private set; }

    public virtual void GenerateUI(Transform parent, UIGenerateOption options)
    {
        _prevParent = transform.parent;
        Options = options;

        if (Options.HasFlag(UIGenerateOption.ClearPanel))
        {
            UIManager.Instance.ClearPanel();
        }

        transform.SetParent(parent);
        Parent = parent;

        if (Options.HasFlag(UIGenerateOption.ResettingPos))
        {
            ((RectTransform)transform).offsetMin = Vector2.zero;
            ((RectTransform)transform).offsetMax = Vector2.zero;
        }

        Active = true;
    }

    public virtual void RemoveUI()
    {
        Active = false;
        transform.SetParent(_prevParent);
        PoolManager.Instance.Push(this);
    }
    
    public override void Init()
    {
        
    }
}