using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradeContainer : MonoBehaviour
{
    [SerializeField] Transform _mainTrm;
    public List<GameObject> UpgradeCards;

    private List<BaseUpgradeElemSO> baseElemInfos;
    private List<PlayerUpgradeElemSO> playerElemInfos;
    private List<TraitUpgradeElemSO> traitElemInfos;

    public void SetUpgrade(GameObject templateItem, EUpgradeType type, int elemNum)
    {
        baseElemInfos = UpgradeManager.Instance.BaseElemInfos;
        playerElemInfos = UpgradeManager.Instance.PlayerElemInfos;
        traitElemInfos = UpgradeManager.Instance.TraitElemInfos;
        
        if (type == EUpgradeType.BASE)
        {
            //base 3개 띄움
            if (transform.childCount == 0)
            {
                for (int i = 0; i < baseElemInfos.Count; i++)
                {
                    GameObject upgradeObj = Instantiate(templateItem, transform);
                    BaseUpgradeCard upgradeUI = upgradeObj.GetComponent<BaseUpgradeCard>();
                    BaseUpgradeElemSO upgradeElem = baseElemInfos[i];
                    upgradeUI.Setting(upgradeElem, null);
                    UpgradeCards.Add(upgradeObj);
                }
            }
            else
                Debug.Log("BaseItem이 이미 존재");
        }
        else
        {
            GameObject upgradeObj = Instantiate(templateItem, transform);
            UpgradeCard upgradeUI = upgradeObj.GetComponent<UpgradeCard>();
            if (type == EUpgradeType.PLAYER)
            {
                EPlayerUpgradeElement etype = (EPlayerUpgradeElement)elemNum;
                Debug.Log(etype);
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
        _mainTrm.gameObject.SetActive(false);
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
