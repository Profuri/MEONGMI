using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class PlayerFeatureChoicePanel : ChoicePanel
{
    [SerializeField] private RectTransform _roulettTrm;
    [SerializeField] private List<TraitUpgradeElemSO> _featureDataList;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    private List<Image> _images;

    private void Awake()
    {
        _images = _roulettTrm.GetComponentsInChildren<Image>().ToList();
        ResetRoulett();
    }

    public TraitUpgradeElemSO ResetRoulett()
    {
        TraitUpgradeElemSO result = null;
        _roulettTrm.anchoredPosition = new Vector2(530f, 0f);
        int prev = -1;
        for (int i = 0; i < _images.Count; i++)
        {
            Image image = _images[i];
            int rand = Random.Range(0, _featureDataList.Count - 1);
            if(rand == prev)
            {
                i--;
                continue;
            }
            image.sprite = _featureDataList[rand].Image;
            if(i == 25)
            {
                result = _featureDataList[rand];
            }
            prev = rand;
        }

        return result;
    }

    //Debug Function
    public void OnRolling()
    {
        Rolling();
    }

    public TraitUpgradeElemSO Rolling(Action callback = null)
    {
        TraitUpgradeElemSO result = ResetRoulett();
        _button.interactable = false;
        _roulettTrm.DOAnchorPosX(-3225f, 4f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _button.interactable = true;
            _title.SetText(result.name);
            _description.SetText(result.Description);
        });

        return result;
    }
}
