using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class InGameHUD : UIComponent
{
    [SerializeField] TextMeshProUGUI baseResourceText;
    //[SerializeField] TextMeshProUGUI playerResourceText;
    [SerializeField] TextMeshProUGUI UnitText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] FeatureInfoPanel traitImage;

    [SerializeField] Slider playerResSlider;
    [SerializeField] Slider baseResSlider;
    [SerializeField] Slider unitSlider;
    [SerializeField] Slider playerHpSlider;

    [SerializeField] Ease sliderEase;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        ReSet();   
    }

    public void Update()
    {
        UpdatePhaseTime();
    }

    private void ReSet()
    {
        UpdateTrait(ETraitUpgradeElement.NONE);
        UpdateBaseResource();
        UpdatePlayerResource();
        UpdatePlayerHP();
        UpdateUnitText();
    }

    public void UpdateBaseResource()
    {
        int curRes = ResManager.Instance.BaseResourceCnt;
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(baseResSlider, curRes, maxRes);
        baseResourceText.text = $"{curRes} / {maxRes}";
    }

    public void UpdatePlayerResource()
    {
        int curRes = ResManager.Instance.PlayerResourceCnt;
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(playerResSlider, curRes, maxRes);
        //playerResourceText.text = curRes.ToString();
    }

    public void UpdatePlayerHP()
    {
        int curHP = (int)GameManager.Instance.PlayerController.CurrentHP;
        int maxHP = (int)GameManager.Instance.PlayerController.GetMaxHP();
        UpdateSlider(playerHpSlider, curHP, maxHP);
        //playerResourceText.text = curRes.ToString();
    }

    public void UpdateUnitText()
    {
        int curRes = GameManager.Instance.Base.CurUnitCount;
        int maxRes = StatManager.Instance.UnitMaxValue;
        UpdateSlider(unitSlider, curRes, maxRes);
        //UnitText.text = $"{curRes} / {maxRes}";
        UnitText.text = curRes.ToString();


    }

    public void UpdatePhaseTime()
    {
        timeText.text = PhaseManager.Instance.GetCurTime().ToString("0");
    }

    public void UpdateTrait(ETraitUpgradeElement trait)
    {
        traitImage.TraitType = trait;
    }

    public void UpdateSlider(Slider slider, float minValue, float maxValue)
    {
        float start = slider.value;
        DOTween.To(() => start, value => slider.value = value, minValue / maxValue, 0.3f).SetEase(sliderEase);
    }

}
