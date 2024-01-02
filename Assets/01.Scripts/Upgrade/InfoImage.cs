using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoImage : MonoBehaviour
{
    private UpgradeElemInfoSO myInfo;
    public UpgradeElemInfoSO Info => myInfo;

    public void Setting(UpgradeElemInfoSO so)
    {
        Debug.Log(so.name);
        myInfo = so;
    }
}
