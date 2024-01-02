using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    private TextMeshProUGUI description;
    private Image image;
    private UpgradeElemInfoSO myInfo;
    public UpgradeElemInfoSO Info => myInfo;

    private Action ReleaseAct;

    public void Setting(UpgradeElemInfoSO so, Action releaseAct)
    {
        Debug.Log(so.name);
        description.text = so.Description;
        image.sprite = so.Image;
        gameObject.name = so.name;
        ReleaseAct = releaseAct;
    }

    public void OnClick()
    {
        //ȿ������

        ReleaseAct?.Invoke();
    }
}
