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
    [SerializeField] RectTransform featureRoot;

    [SerializeField] Slider playerResSlider;
    [SerializeField] Slider baseResSlider;
    [SerializeField] Slider unitSlider;
    [SerializeField] Slider playerHpSlider;

    [SerializeField] Ease sliderEase;
    private List<FeatureInfoPanel> features;
    
    private void Awake()
    {
        features = featureRoot.GetComponentsInChildren<FeatureInfoPanel>().ToList();
    }

    private void Start()
    {
        ReSet();   
    }

    public void Update()
    {
        
    }

    private void ReSet()
    {
        features.ForEach(feature => feature.SetActive(false));
        UnitText.text = $"{0} / {StatManager.Instance.UnitMaxValue}";
    }

    public void UpdateBaseResource()
    {
        int curRes = ResManager.Instance.BaseResourceCnt;
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(baseResSlider, curRes, maxRes);
        baseResourceText.text = ResManager.Instance.BaseResourceCnt.ToString();
    }

    public void UpdatePlayerResource()
    {
        int curRes = ResManager.Instance.PlayerResourceCnt;
        int maxRes = StatManager.Instance.MaxBaseResValue;
        UpdateSlider(playerResSlider, curRes, maxRes);
        //playerResourceText.text = curRes.ToString();
    }

    public void UpdateUnitText()
    {
        int curRes = BaseManager.Instance.CurUnitCount;
        int maxRes = StatManager.Instance.UnitMaxValue;
        UpdateSlider(unitSlider, curRes, maxRes);
        UnitText.text = $"{curRes} / {maxRes}";
    }

    public void UpdatePhaseTime()
    {
        timeText.text = PhaseManager.Instance.GetCurTime().ToString();
    }

    public void UpdateSlider(Slider slider, float minValue, float maxValue)
    {
        float start = slider.value;
        DOTween.To(() => start, value => slider.value = value, minValue / maxValue, 0.3f).SetEase(sliderEase);
    }
    
    public void TraitsBarUpdate(ETraitUpgradeElement trait)
    {
        features.ForEach((feature) =>
        {
            if (trait == feature.GetTraitType)
                feature.SetActive(true);
            else
                feature.SetActive(false);
        });
    }

}
