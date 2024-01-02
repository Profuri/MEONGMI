using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EUpgradeType
{
    BASE = 1, // ����.
    PLAYER = 2, // �÷��̾�.
    TRAIT = 3, //Ư��.
}

public enum EBaseUpgradeElement
{
    NONE = -1,
    UNITCNT = 0, // ���� �� ����.
    LINELEN, // �� ���� ����.
    MAXRES, // �ִ� �ڿ� ����.
    END,
}

public enum EPlayerUpgradeElement
{
    NONE = -1,
    BULLETCOUNTUP = 0, // �Ѿ� �� ����.
    FIRESPEEDUP, // ���� �ӵ� ����.
    MOVESPEEDUP, // �̵� �ӵ� ����.
    COLLECTSPEEDUP, // ���� �ӵ� ����.
    DAMAGE, // ������ ����.
    RECOVERY, //�ʴ� ȸ���� ����.
    LUCK, // ��� ����.
    DEPEND, // ���� ����.
    HP, // ü�� ����.
    END,
}

public enum ETraitUpgradeElement
{
    NONE = -1,
    RESTOBULLET = 0, // �ڿ� �Ѿ˷�.
    CHARGE, // ������.
    PENETRATE, // ����.
    FOLLOW, // ����.
    DOTDAMAGE, // ��Ʈ ��(����).
    STATIC, // ���� ����.
    END,
}

public class UpgradeManager : MonoSingleton<UpgradeManager>
{

    [SerializeField] private int basicUpgradeNeedCnt;
    [field:SerializeField]
    public int BaseUpgradeNeedResCnt { get; set; } 
    [field:SerializeField]
    public int PlayerUpgradeNeedResCnt { get; set; }
    [field:SerializeField]
    public int TraitUpgradeNeedResCnt { get; set; }

    public ETraitUpgradeElement curTraitElem = ETraitUpgradeElement.NONE;

    private int randomCardCount = 3;

    private List<BaseUpgradeElemSO> baseElemInfos;
    private List<PlayerUpgradeElemSO> playerElemInfos;
    private List<TraitUpgradeElemSO> traitElemInfos;

    public List<BaseUpgradeElemSO> BaseElemInfos => baseElemInfos;
    public List<PlayerUpgradeElemSO> PlayerElemInfos => playerElemInfos;
    public List<TraitUpgradeElemSO> TraitElemInfos => traitElemInfos;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        BaseUpgradeNeedResCnt = PlayerUpgradeNeedResCnt = TraitUpgradeNeedResCnt = basicUpgradeNeedCnt;
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
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID�� ������� ���
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
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID�� ������� ���
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
            string assetPath = AssetDatabase.GUIDToAssetPath(assetName); //GUID�� ������� ���
            TraitUpgradeElemSO itemData = AssetDatabase.LoadAssetAtPath<TraitUpgradeElemSO>(assetPath);
            if (itemData != null)
            {
                traitElemInfos.Add(itemData);
                Debug.Log(itemData.name);
            }
        }
    }

    public void Upgrade(EUpgradeType upgradeType)
    {
        //Debug.Log("Upgrade�غ�");
        int upgradeCnt = 0;
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                upgradeCnt = BaseUpgradeNeedResCnt;
                break;
            case EUpgradeType.PLAYER:
                upgradeCnt = PlayerUpgradeNeedResCnt;
                break;
            case EUpgradeType.TRAIT:
                upgradeCnt = TraitUpgradeNeedResCnt;
                break;
            default:
                Debug.LogError("");
                return;
        }

        if(ResManager.Instance.UseResource(upgradeCnt))
        {
            Debug.Log("Upgrade�غ�");
            RandomUpgrade(upgradeType);
            UpdateResNeed(upgradeType);
        }
        else
        {
            TestUIManager.Instance.UpgradeFail();
        }
    }

    private void RandomUpgrade(EUpgradeType upgradeType)
    {
        int maxEclusive = 0;
        List<int> pickedNums = new() { -1 };
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                {
                    TestUIManager.Instance.AddUpgradeElem(upgradeType, 0);
                }
                break;
            case EUpgradeType.PLAYER:
                {
                    maxEclusive = (int)EPlayerUpgradeElement.END;
                    for(int i = 0; i < randomCardCount; i++)
                    {
                        int elemNum = Random.Range(0, maxEclusive);
                        while(true)
                        {
                            if (pickedNums.Contains(elemNum))
                            {
                                elemNum = Random.Range(0, maxEclusive);
                            }
                            else
                                break;
                        }
                        pickedNums.Add(elemNum);
                        Debug.Log(elemNum);
                        TestUIManager.Instance.AddUpgradeElem(upgradeType, elemNum);
                    }
                }
                break;
            case EUpgradeType.TRAIT: // �ߺ� x
                {
                    maxEclusive = (int)ETraitUpgradeElement.END;
                    ETraitUpgradeElement traitElem = curTraitElem;
                    while(traitElem == curTraitElem)
                    {
                        traitElem = (ETraitUpgradeElement)Random.Range(0, maxEclusive);
                    }
                    TestUIManager.Instance.AddUpgradeElem(upgradeType, (int)traitElem);
                }
                break;
        }
    }

    public void UpdateResNeed(EUpgradeType upgradeType)
    {
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                BaseUpgradeNeedResCnt *= 2;
                break;
            case EUpgradeType.PLAYER:
            case EUpgradeType.TRAIT:
                PlayerUpgradeNeedResCnt += PlayerUpgradeNeedResCnt / 2;
                break;
        }
    }

}
