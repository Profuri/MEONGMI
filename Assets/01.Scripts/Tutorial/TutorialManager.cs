using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialInfo
{
    public bool IsClear()
    {
        return false;
    }
}

public class TutorialManager : MonoSingleton<TutorialManager>
{


    public override void Init()
    {
        
    }
}
