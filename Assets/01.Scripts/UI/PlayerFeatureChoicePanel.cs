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
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Image _effectPanel;
    [SerializeField] private Image _effectImage;
    private List<UpgradeCard> _upgradeCards;
    private List<Image> _images;

    private void Awake()
    {
        _images = new List<Image>();
        _upgradeCards = _roulettTrm.GetComponentsInChildren<UpgradeCard>().ToList();
        for(int i = 0; i < _upgradeCards.Count; i++)
        {
            _images.Add(_upgradeCards[i].GetComponent<Image>());
        }
        _particleSystem.Stop();
        
        _effectImage = _effectPanel.transform.Find("Image").GetComponent<Image>();
        ResetRoulett();
    }

    public TraitUpgradeElemSO ResetRoulett()
    {
        TraitUpgradeElemSO result = null;
        _roulettTrm.anchoredPosition = new Vector2(530f, 0f);
        int prev = -1;
        for (int i = 0; i < _upgradeCards.Count; i++)
        {
            int rand = Random.Range(0, _featureDataList.Count - 1);
            _upgradeCards[i].Setting(_featureDataList[rand], null);
            
            Image image = _upgradeCards[i].GetComponent<Image>();
            
            if(rand == prev)
            {
                i--;
                continue;
            }
            
            image.sprite = _featureDataList[rand].Image;
            
            if (i == 25)
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
        _button.interactable = false;
        
        TraitUpgradeElemSO result = ResetRoulett();
        _effectImage.sprite = result.Image;

        _roulettTrm.DOAnchorPosX(-3225f, 4f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            //_button.interactable = true;
            _title.SetText(result.name);
            _description.SetText(result.Description);

            _effectImage.gameObject.SetActive(true);
            _particleSystem.Play();

            Sequence seq = DOTween.Sequence();
            seq.Append(_effectPanel.DOFade(0.7f, 0.25f));
            seq.Insert(0, _effectImage.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.25f));
        });

        return result;
    }
}
