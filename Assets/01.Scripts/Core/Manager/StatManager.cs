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

    [Header("TraitStat")]
    public Dictionary<ETraitUpgradeElement, bool> traitDicionary;

    public int MaxResValue { get; set; }
    public int UnitMaxValue { get; set; }
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
        traitDicionary ??= new Dictionary<ETraitUpgradeElement, bool>();
        foreach (ETraitUpgradeElement type in Enum.GetValues(typeof(ETraitUpgradeElement)))
        {
            traitDicionary.Add(type, false);
        }
    }

    public void BaseInit()
    {
        MaxResValue = BaseStats.MaxResCnt;
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
                MaxResValue += AddMaxResValue;
                break;
        }
    }

    public void SetTrait(ETraitUpgradeElement elem)
    {
        foreach (var pair in traitDicionary)
        {
            traitDicionary[pair.Key] = false;
        };

        traitDicionary[elem] = true;
    }

    public ETraitUpgradeElement GetCurTrait()
    {
        foreach (var pair in traitDicionary)
        {
            if(pair.Value)
                return pair.Key;
        };

        return ETraitUpgradeElement.NONE;
    }
}