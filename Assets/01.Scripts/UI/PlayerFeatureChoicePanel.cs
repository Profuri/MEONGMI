using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class PlayerFeatureChoicePanel : UIComponent
{
    [SerializeField] private GameObject _mainTrm;
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
        
        _effectImage = _effectPanel.transform.Find("Image").GetComponent<Image>();
    }

    public override void GenerateUI(Transform parent)
    {
        base.GenerateUI(parent);
        Initialize();
    }

    protected override void GenerateTransition()
    {
        ((RectTransform)transform).DOScaleY(1, 0.5f);
    }

    protected override void RemoveTransition(Action callback)
    {
        ((RectTransform)transform).DOScaleY(0, 0.5f).OnComplete(() => callback?.Invoke());
    }

    private void Initialize()
    {
        _title.SetText("Ư�� ����");
        _description.SetText("����� �����?");

        _button.interactable = true;
        _particleSystem.Stop();
        _particleSystem.gameObject.SetActive(false);

        _effectImage.transform.DOScale(Vector3.one, 0f);
        _effectImage.gameObject.SetActive(false);
        Color color = _effectPanel.color;
        _effectPanel.color = new Color(color.r, color.g, color.b, 0);
    }

    public TraitUpgradeElemSO ResetRoulett()
    {
        TraitUpgradeElemSO result = null;
        _roulettTrm.anchoredPosition = new Vector2(530f, 0f);
        int prev = -1;
        for (int i = 0; i < _upgradeCards.Count; i++)
        {
            int rand = Random.Range(0, _featureDataList.Count - 1);
            if(_featureDataList[rand].Type == UpgradeManager.Instance.curTraitElem)
            {
                i--;
                continue;
            }
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

        UpgradeManager.Instance.SetCurTraitElem(result.Type);
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
            _title.SetText(result.Name);
            _description.SetText(result.Description);

            _effectImage.gameObject.SetActive(true);
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();

            Sequence seq = DOTween.Sequence();
            seq.Append(_effectPanel.DOFade(0.7f, 0.25f));
            seq.Insert(0, _effectImage.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.25f));
            seq.AppendInterval(1.5f);
            seq.OnComplete(() =>
            {
                UpgradeManager.Instance.ApplyUpgradeTrait(result.Type);
                Debug.Log(result.Type.ToString());
                _mainTrm.SetActive(false);
            });
        });

        return result;
    }
}
