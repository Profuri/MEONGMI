using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Base/Stat")]
public class BaseStatSO : ScriptableObject
{
    public int MaxResCnt;
    public int PlayerMaxResCnt;
    public int UnitMaxCnt;
    public float MovementRange;
}
