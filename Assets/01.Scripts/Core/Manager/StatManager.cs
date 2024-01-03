using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoSingleton<StatManager>
{
    [SerializeField] private PlayerStat PlayerStats;
    [SerializeField] private BaseStatSO BaseStats;
    
    [Header("PlayerStat")]
    [SerializeField] private float AddBulletCntValue;
    [SerializeField] private float AddFireSpeedUpValue;
    [SerializeField] private float AddMoveSpeedUpValue;
    [SerializeField] private float AddCollectSpeedUpValue;
    [SerializeField] private float AddDamageUpValue;
    [SerializeField] private float AddRecoveryUpValue;
    [SerializeField] private float AddLuckUpValue;
    [SerializeField] private float AddDefendUpValue;
    [SerializeField] private float AddHpUpValue;
    
    [Header("BaseStat")]
    [SerializeField] private int AddMaxResValue;
    [SerializeField] private int AddUnitMaxValue;
    [SerializeField] private float AddMovementRangeValue;

   // [Header("TraitStat")]
    public Dictionary<ETraitUpgradeElement, bool> TraitDicionary;

    public ETraitUpgradeElement CurTrait;

    [field:SerializeField]
    public int MaxBaseResValue { get; set; }
    [field:SerializeField]
    public int UnitMaxValue { get; set; }
    [field:SerializeField]
    public float MovementRange { get; set; }

  
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        PlayerInit();
        SetTraitInit();
        BaseInit();
    }


    public void SetTraitInit()
    {
        TraitDicionary = new Dictionary<ETraitUpgradeElement, bool>();
        foreach (ETraitUpgradeElement type in Enum.GetValues(typeof(ETraitUpgradeElement)))
        {
            Debug.Log(type.ToString());
            TraitDicionary.Add(type, false);
        }
        CurTrait = ETraitUpgradeElement.NONE;
    }

    public void BaseInit()
    {
        MaxBaseResValue = BaseStats.MaxResCnt;
        UnitMaxValue = BaseStats.UnitMaxCnt;
        MovementRange = BaseStats.MovementRange;
    }

    public void PlayerInit()
    {
        PlayerStats.health.RemoveAll();
        PlayerStats.armor.RemoveAll();
        PlayerStats.damage.RemoveAll();
        PlayerStats.recovery.RemoveAll();
        PlayerStats.luck.RemoveAll();
        PlayerStats.shotCnt.RemoveAll();
        PlayerStats.shotDelay.RemoveAll();
        PlayerStats.moveSpeed.RemoveAll();
        PlayerStats.chargingSpeed.RemoveAll();
        PlayerStats.gatheringSpeed.RemoveAll();
    }

    public void AddModifier(EPlayerUpgradeElement elem)
    {
        switch (elem)
        {
            case EPlayerUpgradeElement.BULLETCOUNTUP:
                PlayerStats.shotCnt.AddModifier(AddBulletCntValue);
                break;
            case EPlayerUpgradeElement.FIRESPEEDUP:
                PlayerStats.shotDelay.AddModifier(AddFireSpeedUpValue);
                break;
            case EPlayerUpgradeElement.MOVESPEEDUP:
                PlayerStats.moveSpeed.AddModifier(AddMoveSpeedUpValue);
                break;
            case EPlayerUpgradeElement.COLLECTSPEEDUP:
                PlayerStats.gatheringSpeed.AddModifier(AddCollectSpeedUpValue);
                break;
            case EPlayerUpgradeElement.DAMAGE:
                PlayerStats.damage.AddModifier(AddDamageUpValue);
                break;
            case EPlayerUpgradeElement.RECOVERY:
                PlayerStats.recovery.AddModifier(AddRecoveryUpValue);
                break;
            case EPlayerUpgradeElement.LUCK:
                PlayerStats.luck.AddModifier(AddLuckUpValue);
                break;
            case EPlayerUpgradeElement.DEPEND:
                PlayerStats.armor.AddModifier(AddDefendUpValue);
                break;
            case EPlayerUpgradeElement.HP:
                PlayerStats.health.AddModifier(AddHpUpValue);
                break;
        }
    }

    public void AddBaseStat(EBaseUpgradeElement elem)
    {
        switch(elem)
        {
            case EBaseUpgradeElement.UNITCNT:
                UnitMaxValue += AddUnitMaxValue;
                break;
            case EBaseUpgradeElement.LINELEN:
                MovementRange += AddMovementRangeValue;
                break;
            case EBaseUpgradeElement.MAXRES:
                MaxBaseResValue += AddMaxResValue;
                break;
        }
    }

    public void SetTrait(ETraitUpgradeElement elem)
    {
        Debug.Log(TraitDicionary.Count);
        //foreach (var pair in TraitDicionary)
        //{
        //    Debug.Log(pair);
        //    TraitDicionary[pair.Key] = false;
        //};

        for(int i = -1; i < TraitDicionary.Count - 1; i++)
        {
            TraitDicionary[(ETraitUpgradeElement)i] = false;
        }

        TraitDicionary[elem] = true;

        CurTrait = elem;
        
        TestUIManager.Instance.IngameHUD.TraitsBarUpdate(CurTrait);
    }

    public ETraitUpgradeElement GetCurTrait()
    {
        foreach (var pair in TraitDicionary)
        {
            if (pair.Key == ETraitUpgradeElement.NONE || pair.Key == ETraitUpgradeElement.END)
                continue;

            if(pair.Value)
                return pair.Key;
        };

        return ETraitUpgradeElement.NONE;
    }
}