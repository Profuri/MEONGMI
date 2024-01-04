using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TestUIManager : MonoSingleton<TestUIManager>
{
    [Header("Root")]
    [SerializeField] InGameHUD _IngameHUD;
    public InGameHUD IngameUI => _IngameHUD;   


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

    [Header("Cost")]
    [SerializeField] TextMeshProUGUI _playerCostTxt; 
    [SerializeField] TextMeshProUGUI _traitCostTxt; 


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

    public void SetPlayerCostTxt(int cost) => _playerCostTxt.text = cost.ToString();
    public void SetTraitCostTxt(int cost) => _traitCostTxt.text = cost.ToString();

    public void UpgradeRootPanelOn() => UpgradeUIPanel.SetActive(true);
    

    public void BaseUpgradePanel()
    {
        UpgradeManager.Instance.AddElement(EUpgradeType.BASE);
        _BaseUpgradePanel.SetActive(true);
    }
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
                //traitUpgradeContainer.SetUpgrade(traitCardTemplate, upgradeType, elem);
                break;
        }
    }
}