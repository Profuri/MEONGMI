using UnityEngine;
using Random = UnityEngine.Random;

public enum EUpgradeType
{
    BASE = 1, // 기지.
    PLAYER = 2, // 플레이어.
    TRAIT = 3, //특성.
}

public enum EBaseUpgradeElement
{
    NONE = -1,
    UNITCNT = 0, // 유닛 수 증가.
    LINELEN, // 줄 길이 증가.
    MAXRES, // 최대 자원 증가.
    END,
}

public enum EPlayerUpgradeElement
{
    NONE = -1,
    BULLETCOUNT = 0, // 총알 수 증가.
    FIRESPEEDUP, // 연사 속도 증가.
    MOVESPEEDUP, // 이동 속도 증가.
    COLLECTSPEEDUP, // 수집 속도 증가.
    DAMAGE, // 데미지 증가.
    RECOVERY, //초당 회복량 증가.
    LUCK, // 행운 증가.
    DEPEND, // 방어력 증가.
    HP, // 체력 증가.
    END,
}

public enum ETraitUpgradeElement
{
    NONE = -1,
    RESTOBULLET = 0, // 자원 총알로.
    CHARGE, // 차지샷.
    PENETRATE, // 관통.
    FOLLOW, // 유도.
    DOTDAMAGE, // 도트 뎀(독뎀).
    STATIC, // 전이 공격.
    END,
}

public class UpgradeManager : MonoSingleton<UpgradeManager>
{

    [SerializeField] private int basicUpgradeNeedCnt;
    public int BaseUpgradeNeedResCnt { get; set; } 
    public int PlayerUpgradeNeedResCnt { get; set; }
    public int TraitUpgradeNeedResCnt { get; set; }

    public ETraitUpgradeElement curTraitElem = ETraitUpgradeElement.NONE; 

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
                    int elemNum = Random.Range(0, maxEclusive);
                    TestUIManager.Instance.SetUpgradeElem(upgradeType, elemNum);
                }
                break;
            case EUpgradeType.TRAIT:
                {
                    maxEclusive = (int)ETraitUpgradeElement.END;
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
