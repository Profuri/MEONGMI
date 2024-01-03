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

    public virtual void Setting(UpgradeElemInfoSO so, Action releaseAct)
    {
        //Debug.Log(so.Name);
        //description.text = so.Description;
        //image.sprite = so.Image;
        myInfo = so;
        gameObject.name = so.Name;
        ReleaseAct = releaseAct;
    }

    public virtual void OnClick()
    {
        //효과적용
        ReleaseAct?.Invoke();
    }
}
