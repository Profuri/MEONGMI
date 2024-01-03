using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class InGameHUD : UIComponent
{
    [SerializeField] TextMeshProUGUI baseResourceText;
    [SerializeField] TextMeshProUGUI playerResourceText;
    [SerializeField] TextMeshProUGUI UnitText;
    [SerializeField] RectTransform featureRoot;

    List<FeatureInfoPanel> features;
    
    private void Awake()
    {
        features = featureRoot.GetComponentsInChildren<FeatureInfoPanel>().ToList();
    }

    private void Start()
    {
        ReSet();   
    }

    private void ReSet()
    {
        features.ForEach(feature => feature.SetActive(false));
        UnitText.text = $"{0} / {StatManager.Instance.UnitMaxValue}";
    }

    public void UpdateBaseResourceText()
    {
        baseResourceText.text = ResManager.Instance.BaseResourceCnt.ToString();
    }

    public void UpdatePlayerResourceText()
    {
        playerResourceText.text = ResManager.Instance.ResourceCnt.ToString();
    }

    public void UpdateUnitText()
    {
        UnitText.text = $"{BaseManager.Instance.CurUnitCount} / {StatManager.Instance.UnitMaxValue}";
    }

    public void UpdateStats()
    {

    }

    public void UpdatePhaseTime()
    {

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
