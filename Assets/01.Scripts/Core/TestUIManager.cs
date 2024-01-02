using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestUIManager : MonoSingleton<TestUIManager>
{
    [SerializeField] GameObject UpgradeUIPanel;

    [SerializeField] GameObject container;
    [SerializeField] GameObject templateItem;
    [SerializeField] GameObject upgradeFailPanel;


    List<BaseUpgradeElemSO> baseElemInfos;
    List<PlayerUpgradeElemSO> playerElemInfos;
    List<TraitUpgradeElemSO> traitElemInfos;

    

    public override void Init()
    {
    }

    public void Awake()
    {
        LoadUpdateInfos();
    }

    private void LoadUpdateInfos()
    {
        baseElemInfos = new();
        playerElemInfos = new();
        traitElemInfos = new();

        string[] baseAssetNames = AssetDatabase.FindAssets("", new[] { "Assets/04.SO/Upgrade/Base" });

        foreach (string assetName in baseAssetNames)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID를 기반으로 경로
            BaseUpgradeElemSO itemData = AssetDatabase.LoadAssetAtPath<BaseUpgradeElemSO>(assetPath);
            if (itemData != null)
            {
                baseElemInfos.Add(itemData);
                Debug.Log(itemData.name);
            }
        }

        string[] playerAssetNames = AssetDatabase.FindAssets("", new[] { "Assets/04.SO/Upgrade/Player" });

        foreach (string assetName in playerAssetNames)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID를 기반으로 경로
            PlayerUpgradeElemSO itemData = AssetDatabase.LoadAssetAtPath<PlayerUpgradeElemSO>(assetPath);
            if (itemData != null)
            {
                playerElemInfos.Add(itemData);
                Debug.Log(itemData.name);
            }
        }

        string[] traitAssetNames = AssetDatabase.FindAssets("", new[] { "Assets/04.SO/Upgrade/Trait" });

        foreach (string assetName in traitAssetNames)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID를 기반으로 경로
            TraitUpgradeElemSO itemData = AssetDatabase.LoadAssetAtPath<TraitUpgradeElemSO>(assetPath);
            if (itemData != null)
            {
                traitElemInfos.Add(itemData);
                Debug.Log(itemData.name);
            }
        }
    }

    public void UpgradeBase() => UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    public void UpgradePlayer() => UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    public void UpgradeTrait() => UpgradeManager.Instance.Upgrade(EUpgradeType.TRAIT);
    
    public void UpgradeFail()
    {
        // 자원이 부족합니다 UI띄어줘야 함.
        upgradeFailPanel.SetActive(true);
    }

    public void SetUpgradeElem(EUpgradeType type, int elemNum) // 3개 띄워줘야 함
    {
        if(type == EUpgradeType.BASE)
        {
            //base 3개 띄움
            for(int i = 0; i < baseElemInfos.Count; i++)
            {
                GameObject upgradeObj = Instantiate(templateItem, container.transform);
                UpgradeUI upgradeUI = upgradeObj.GetComponent<UpgradeUI>();
                BaseUpgradeElemSO upgradeElem = baseElemInfos[i];
                upgradeUI.Setting(upgradeElem);
            }
        }
        else
        {
            GameObject upgradeObj = Instantiate(templateItem, container.transform);
            UpgradeUI upgradeUI = upgradeObj.GetComponent<UpgradeUI>();
            if(type == EUpgradeType.PLAYER)
            {
                EPlayerUpgradeElement etype = (EPlayerUpgradeElement)elemNum;
                PlayerUpgradeElemSO player = playerElemInfos.Find((info) => info.Type == etype);
                if (player != null)
                    upgradeUI.Setting(player);
                else
                    Debug.LogError("1");
            }
            else if (type == EUpgradeType.TRAIT)
            {
                ETraitUpgradeElement etype = (ETraitUpgradeElement)elemNum;
                TraitUpgradeElemSO trait = traitElemInfos.Find((info) => info.Type == etype);

                if (trait != null)
                    upgradeUI.Setting(trait);
                else
                    Debug.LogError("1");
            }

            //UpgradeElemInfoSO elemInfo = elemInfos.Find()
            //각 타입의 elemNum의 upgrade 를 띄움
        }
    }
}
