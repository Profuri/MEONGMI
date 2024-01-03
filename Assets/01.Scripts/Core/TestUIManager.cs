using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestUIManager : MonoSingleton<TestUIManager>
{
    [Header("Root")]
    [SerializeField] GameObject UpgradeUIPanel;
    [SerializeField] GameObject _PlayerAndTraitPanel;

    [Header("Container")]
    [SerializeField] UpgradeContainer playerUpgradeContainer;
    [SerializeField] UpgradeContainer baseUpgradeContainer;
    [SerializeField] UpgradeContainer traitUpgradeContainer;

    [Header("CardTemplate")]
    [SerializeField] GameObject baseCardTemplate;
    [SerializeField] GameObject playerCardTemplate;
    [SerializeField] GameObject traitCardTemplate;

    [Header("Panel")]
    [SerializeField] GameObject upgradeFailPanel;
    [SerializeField] GameObject _BaseUpgradePanel;
    [SerializeField] GameObject _PlayerUpgradePanel;
    [SerializeField] GameObject _TraitUpgradePanel;

    public override void Init()
    {
    }

    public void UpgradeBase()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    }
    public void UpgradePlayer()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    }
    public void UpgradeTrait()
    {
        UpgradeManager.Instance.Upgrade(EUpgradeType.TRAIT);
    }

    public void BaseUpgradePanel() => _BaseUpgradePanel.SetActive(true);
    public void PlayerUpgradePanel() => _PlayerUpgradePanel.SetActive(true);
    public void TraitUpgradePanel() => _TraitUpgradePanel.SetActive(true);

    public void PlayerAndTraitPanel() => _PlayerAndTraitPanel.SetActive(true);   
    public void UpgradeFail()
    {
        // 자원이 부족합니다 UI띄어줘야 함.
        upgradeFailPanel.SetActive(true);
    }

    public void AddUpgradeElem(EUpgradeType upgradeType, int elem)
    {
        switch (upgradeType)
        {
            case EUpgradeType.BASE:
                baseUpgradeContainer.SetUpgrade(baseCardTemplate, upgradeType, elem);
                break;
            case EUpgradeType.PLAYER:
                playerUpgradeContainer.SetUpgrade(playerCardTemplate, upgradeType, elem);
                break;
            case EUpgradeType.TRAIT:
                traitUpgradeContainer.SetUpgrade(traitCardTemplate, upgradeType, elem);
                break;
        }
    }
}