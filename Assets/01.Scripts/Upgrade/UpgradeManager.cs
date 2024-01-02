using System.Collections.Generic;
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
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        BaseUpgradeNeedResCnt = PlayerUpgradeNeedResCnt = TraitUpgradeNeedResCnt = basicUpgradeNeedCnt;
    }

    public void Upgrade(EUpgradeType upgradeType)
    {
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
        List<int> pickedNums = new();
        switch(upgradeType)
        {
            case EUpgradeType.BASE:
                {
                    TestUIManager.Instance.SetUpgradeElem(upgradeType, 0);
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
                        TestUIManager.Instance.SetUpgradeElem(upgradeType, elemNum);
                    }
                }
                break;
            case EUpgradeType.TRAIT: // �ߺ� x
                {
                    ETraitUpgradeElement traitElem = curTraitElem;
                    while(traitElem == curTraitElem)
                    {
                        traitElem = (ETraitUpgradeElement)Random.Range(0, maxEclusive);
                    }
                    TestUIManager.Instance.SetUpgradeElem(upgradeType, (int)traitElem);
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
