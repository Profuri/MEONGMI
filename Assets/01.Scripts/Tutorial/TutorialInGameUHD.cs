using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialInGameUHD : UIComponent
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
    

    private TextMeshProUGUI _phaseText;
    


    [SerializeField] private int _maxResCnt;
    [SerializeField] private int _maxPlayerResCnt;
    [SerializeField] private int _unitMaxCnt;
    public int PlayerResourceCnt { get; set; }
    public int BaseResourceCnt { get; set; }
    public int CurUnitCnt { get; set; }
    
    
    private void Awake()
    {
        CameraManager.Instance.Init();
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

        _phaseText = timeText.transform.parent.Find("Text_Phase").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateBaseResource()
    {
        int curRes = BaseResourceCnt;
        int maxRes = _maxResCnt;
        UpdateSlider(baseResSlider, curRes, maxRes);
        baseResourceText.text = $"{curRes} / {maxRes}";
    }

    public void UpdatePlayerResource()
    {
        int curRes = PlayerResourceCnt;
        int maxRes = _maxPlayerResCnt;
        UpdateSlider(playerResSlider, curRes, maxRes);
    }

    public void UpdatePlayerHP()
    {
        int curHP = 10;
        int maxHP = 10;
        UpdateSlider(playerHpSlider, curHP, maxHP);
    }

    public void UpdateUnitText()
    {
        int curRes = CurUnitCnt;
        int maxRes = _unitMaxCnt;
        UpdateSlider(unitSlider, curRes, maxRes);
        UnitText.text = curRes.ToString();
    }

    public void UpdatePhaseTime()
    {
        _phaseText.text = " Remain Monster";
        timeText.SetText("9999");
    }

    public void UpdateTrait(ETraitUpgradeElement trait)
    {
        traitImage.TraitType = trait;
    }

    public void UpdateSlider(Slider slider, float minValue, float maxValue, float time = 1f)
    {
        float start = slider.value;
        DOTween.To(() => start, value => slider.value = value, minValue / maxValue, time).SetEase(sliderEase);
    }

}
