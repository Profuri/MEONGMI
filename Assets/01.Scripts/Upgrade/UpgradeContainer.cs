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
            //base 3�� ���
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
                Debug.Log("BaseItem�� �̹� ����");
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
            //�� Ÿ���� elemNum�� upgrade �� ���
        }
    }

    public void ReleaseUpgrade()
    {
        UpgradeCards.ForEach((item) =>
        {
            Destroy(item);
        });
        UpgradeCards.Clear();
        UIManager.Instance.ChangeUI("InGameHUD");
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
