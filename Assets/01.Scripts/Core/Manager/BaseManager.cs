using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoSingleton<BaseManager>
{
    private int curUnitCount;
    public int CurUnitCount
    {
        get { return curUnitCount; }
        private set { Mathf.Clamp(curUnitCount, 0, StatManager.Instance.UnitMaxValue); }
    }

    private int curResCount;
    public int CurResCount
    {
        get { return curResCount; }
        private set { Mathf.Clamp(curResCount, 0, StatManager.Instance.MaxBaseResValue); }
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        curUnitCount = 0;
        curResCount = 0;
    }

    public void AddUnit()
    {
        curUnitCount++;
    }

    public void AddRes(int resCount)
    {
        curResCount += resCount;
    }
}

