using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestUIManager : MonoSingleton<TestUIManager>
{
    [SerializeField] GameObject UpgradeUIPanel;

    [SerializeField] UpgradeContainer container;
    [SerializeField] GameObject templateUpgradeCard;
    [SerializeField] GameObject upgradeFailPanel;

    [SerializeField] GameObject _BaseUpgradePanel;
    [SerializeField] GameObject _PlayerUpgradePanel;

    public override void Init()
    {
    }  

    public void UpgradeBase() => UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    public void UpgradePlayer() => UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    public void UpgradeTrait() => UpgradeManager.Instance.Upgrade(EUpgradeType.TRAIT);

    public void BaseUpgradePanel() => _BaseUpgradePanel.SetActive(true);
    public void PlayerUpgradePanel() => _PlayerUpgradePanel.SetActive(true);   
    public void UpgradeFail()
    {
        // 자원이 부족합니다 UI띄어줘야 함.
        upgradeFailPanel.SetActive(true);
    }

    public void AddUpgradeElem(EUpgradeType upgradeType, int elem)
    {
        container.SetUpgrade(templateUpgradeCard, upgradeType, elem);
    }
}