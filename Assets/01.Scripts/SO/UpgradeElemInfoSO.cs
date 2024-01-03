using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeElemInfoSO : ScriptableObject
{
    public EUpgradeType UpgradeType;
    //public bool canDuplication;
    [TextArea]
    public string Description;
    public Sprite Image;
   
//#if UNITY_EDITOR
//    public void OnValidate()
//    {
//        if (UpgradeType == EUpgradeType.TRAIT)
//            canDuplication = false;
//        else
//            canDuplication = true;
//    }
//#endif

}
