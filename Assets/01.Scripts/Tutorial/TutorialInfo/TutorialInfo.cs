using UnityEngine;

public abstract class TutorialInfo : ScriptableObject
{
    public string infoText;
    protected bool _isClear;

    public virtual bool IsClear()
    {
        return _isClear;
    }

    public abstract void TutorialUpdate();
}