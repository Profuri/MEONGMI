using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UpgradeInfo")]
public class UpgradeElemInfoSO : ScriptableObject
{
    public EUpgradeType UpgradeType;
    public bool canDuplication;

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (UpgradeType == EUpgradeType.TRAIT)
            canDuplication = false;
        else
            canDuplication = true;
    }
#endif

}
