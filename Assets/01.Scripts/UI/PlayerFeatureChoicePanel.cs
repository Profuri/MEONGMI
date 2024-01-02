using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFeatureChoicePanel : ChoicePanel
{
    [SerializeField] private RectTransform _roulettTrm;
    [SerializeField] private List<Sprite> _featureIconList;
    [SerializeField] private Button _button;
    private List<Image> _images;

    private void Awake()
    {
        _images = _roulettTrm.GetComponentsInChildren<Image>().ToList();
        ResetRoulett();
    }

    public void ResetRoulett()
    {
        _roulettTrm.anchoredPosition = new Vector2(530f, 0f);
        int prev = -1;
        for (int i = 0; i < _images.Count; i++)
        {
            Image image = _images[i];
            int rand = Random.Range(0, _featureIconList.Count - 1);
            if(rand == prev)
            {
                i--;
                continue;
            }
            image.sprite = _featureIconList[rand];
            prev = rand;
        }
    }

    public void Rolling()
    {
        ResetRoulett();
        _button.interactable = false;
        _roulettTrm.DOAnchorPosX(-3225f, 4f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            _button.interactable = true;
            Debug.Log(_images[25].sprite.name);
        });
    }
}
