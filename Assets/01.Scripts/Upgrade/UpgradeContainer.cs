using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour
{
    public List<GameObject> UpgradeCards;
    public Transform mainTrm;

    public void SetUpgrade(GameObject templateItem, EUpgradeType type, int elemNum)
    {
        this.gameObject.SetActive(true);

        List<BaseUpgradeElemSO> baseElemInfos = UpgradeManager.Instance.BaseElemInfos;
        List<PlayerUpgradeElemSO> playerElemInfos = UpgradeManager.Instance.PlayerElemInfos;
        List<TraitUpgradeElemSO> traitElemInfos = UpgradeManager.Instance.TraitElemInfos;
     
        if (type == EUpgradeType.BASE)
        {
            //base 3개 띄움
            for (int i = 0; i < baseElemInfos.Count; i++)
            {
                GameObject upgradeObj = Instantiate(templateItem, transform);
                UpgradeCard upgradeUI = upgradeObj.GetComponent<UpgradeCard>();
                BaseUpgradeElemSO upgradeElem = baseElemInfos[i];
                upgradeUI.Setting(upgradeElem, ReleaseUpgrade);
                UpgradeCards.Add(upgradeObj);
            }
        }
        else
        {
            GameObject upgradeObj = Instantiate(templateItem, transform);
            UpgradeCard upgradeUI = upgradeObj.GetComponent<UpgradeCard>();
            if (type == EUpgradeType.PLAYER)
            {
                EPlayerUpgradeElement etype = (EPlayerUpgradeElement)elemNum;
                PlayerUpgradeElemSO player = playerElemInfos.Find((info) => info.Type == etype);
                if (player != null)
                    upgradeUI.Setting(player, ReleaseUpgrade);
                else
                    Debug.LogError("1");
            }
            else if (type == EUpgradeType.TRAIT)
            {
                ETraitUpgradeElement etype = (ETraitUpgradeElement)elemNum;
                TraitUpgradeElemSO trait = traitElemInfos.Find((info) => info.Type == etype);

                if (trait != null)
                    upgradeUI.Setting(trait, ReleaseUpgrade);
                else
                    Debug.LogError("1");
            }

            UpgradeCards.Add(upgradeObj);
            //각 타입의 elemNum의 upgrade 를 띄움
        }
    }

    public void ReleaseUpgrade()
    {
        UpgradeCards.ForEach((item) =>
        {
            Destroy(item);
        });
        UpgradeCards.Clear();
        mainTrm.gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        UpgradeCards.ForEach((item) =>
        {
            Destroy(item);
        });
        UpgradeCards.Clear();
    }

}
