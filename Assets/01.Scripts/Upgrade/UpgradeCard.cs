using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    private UpgradeElemInfoSO myInfo;
    public UpgradeElemInfoSO Info => myInfo;

    private Action ReleaseAct;

    public void Setting(UpgradeElemInfoSO so, Action releaseAct)
    {
        Debug.Log(so.name);
        //description.text = so.Description;
        //image.sprite = so.Image;
        myInfo = so;
        gameObject.name = so.name;
        ReleaseAct = releaseAct;
    }

    public void OnClick()
    {
        //효과적용

        ReleaseAct?.Invoke();
    }
}
