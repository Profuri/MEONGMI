using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseUpgradeCard : UpgradeCard
{
    private TextMeshProUGUI description;
    private TextMeshProUGUI value;
    private TextMeshProUGUI addValue;
    private TextMeshProUGUI moneyTxt;
    
    private Image upgradeImg;
    private Image resourceImg;

    BaseUpgradeElemSO info;
    int curCost;

    public override void Setting(UpgradeElemInfoSO so, Action releaseAct)
    {
        base.Setting(so, releaseAct);
        
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        value = transform.Find("Value").GetComponent<TextMeshProUGUI>();
        addValue = transform.Find("AddValue").GetComponent<TextMeshProUGUI>();
        moneyTxt = transform.Find("Button/Group/MoneyTxt").GetComponent<TextMeshProUGUI>();

        resourceImg = transform.Find("Button/Group/Resource").GetComponent<Image>();
        upgradeImg = transform.Find("Image").GetComponent<Image>();

        info = so as BaseUpgradeElemSO;

        description.text = info.Description;
        value.text = info.BaseValue.ToString();
        addValue.text = info.AddValue.ToString();
        upgradeImg.sprite = info.Image;
        resourceImg.sprite = info.NeedResource;
        
        curCost = info.BaseNeedCost;
        moneyTxt.text = curCost.ToString();
    }

    public override void OnClick()
    {
        if (UpgradeManager.Instance.BaseUpgrade(info.Type, curCost))
        {
            Debug.Log("Gang");
            UpdateUI();
        }else
           TestUIManager.Instance.UpgradeFail();
    }

    private void UpdateUI()
    {
        Debug.Log($"{gameObject.name} : {curCost}");
        curCost = UpgradeManager.Instance.GetBaseUpgradeMoney(info.Type);
        moneyTxt.text = curCost.ToString();
    }
}
