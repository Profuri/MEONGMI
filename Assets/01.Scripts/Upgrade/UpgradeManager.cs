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
    SLOW, // ������.
    PENETRATE, // ����.
    FOLLOW, // ����.
    DOTDAMAGE, // ��Ʈ ��(����).
    STATIC, // ���� ����.
    END,
}

public class UpgradeManager : MonoSingleton<UpgradeManager>
{

    [SerializeField] private int basicUpgradeNeedCnt;
    public Dictionary<EBaseUpgradeElement, int> BaseUpgradeNeedResCntDic { get; set; } 
    public int PlayerUpgradeNeedResCnt { get; set; }
    public int TraitUpgradeNeedResCnt { get; set; }

    public ETraitUpgradeElement curTraitElem = ETraitUpgradeElement.NONE;

    private int randomCardCount = 3;

    [SerializeField]
    private List<BaseUpgradeElemSO> baseElemInfos;
    
    [SerializeField]
    private List<PlayerUpgradeElemSO> playerElemInfos;
    
    [SerializeField]
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
        //�ʿ� �Ӵ� �ʱ�ȭ
        BaseUpgradeNeedResCntDic = new();
        foreach (var elem in baseElemInfos)
        {
            BaseUpgradeNeedResCntDic.Add(elem.Type, elem.BaseNeedCost);
        }
        PlayerUpgradeNeedResCnt = TraitUpgradeNeedResCnt = basicUpgradeNeedCnt;

    }
    
    public int GetBaseUpgradeMoney(EBaseUpgradeElement elem) => BaseUpgradeNeedResCntDic[elem];
    private void SetBaseCost(EBaseUpgradeElement elem, int value) => BaseUpgradeNeedResCntDic[elem] += value;

    //���߿� �����丵
    public bool BaseUpgrade(EBaseUpgradeElement type, int curCost)
    {
        if(ResManager.Instance.UseResource(curCost))
        {
            int addCost = baseElemInfos.Find((elem) => elem.Type == type).AddCost;
            SetBaseCost(type, addCost);
            return true;
        }
        return false;
    }

    public void Upgrade(EUpgradeType upgradeType)
    {
        //Debug.Log("Upgrade�غ�");
        int upgradeCnt = 0;
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                //upgradeCnt = BaseUpgradeNeedResCnt;
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

        AddElement(upgradeType);
    }

    public void AddElement(EUpgradeType upgradeType)
    {
        int maxEclusive = 0;
        List<int> pickedNums = new() { -1 };
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                {
                    UIManager.Instance.AddUpgradeElem(upgradeType, 0);
                    //Debug.LogError("�ߺ�! �����Դϴ�1");
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
                        UIManager.Instance.AddUpgradeElem(upgradeType, elemNum);
                    }
                }
                break;
        }
    }

    public void SetCurTraitElem(ETraitUpgradeElement elem) => curTraitElem = elem;

    public void ApplyUpgradeTrait(ETraitUpgradeElement upgradeElem)
    {
        StatManager.Instance.SetTrait(upgradeElem);
    }

    public void ApplyUpgradePlayer(EPlayerUpgradeElement upgradeElem)
    {
        StatManager.Instance.AddModifier(upgradeElem);
    }

    public void ApplyUpgradeBase(EBaseUpgradeElement upgradeElem)
    {
        StatManager.Instance.AddBaseStat(upgradeElem);
    }
}