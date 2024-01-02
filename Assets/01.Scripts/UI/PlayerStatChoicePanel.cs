using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStatChoicePanel : ChoicePanel
{
    [SerializeField] private RectTransform _imageListTrm;
    [SerializeField] private UpgradeContainer _upgradeContainer;
    //[SerializeField] private List<InfoImage> _statIconImages;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private EUpgradeType _upgradeType;
    [SerializeField] private GameObject _backglow;
    [SerializeField] private Button _button;

    private UpgradeCard _upgradeCard;
    private bool _isRolling = false;

    private void Awake()
    {
        _title.SetText(string.Empty);
        Init();
    }

    private void Init()
    {
        _upgradeCard = transform.GetComponent<UpgradeCard>();
        _backglow.SetActive(false);
        _button.enabled = false;
    }

    public void RollImage()
    {
        if (_isRolling) return;
        _isRolling = true;

        Image[] imageList = ShuffleArray(_imageListTrm.GetComponentsInChildren<Image>());

        _imageListTrm.DOAnchorPosY((_imageListTrm.childCount - 1) * 235f, 1.5f)
        .OnComplete(() =>
        {
            EndRolling(imageList[0]);
        });
    }

    private void EndRolling(Image imageContainer)
    {
        imageContainer.sprite = _upgradeCard.Info.Image;
        _imageListTrm.anchoredPosition = Vector2.zero;
        _title.SetText(_upgradeCard.Info.Description);
        _button.enabled = true;
        _backglow.SetActive(true);
        _isRolling = false;
    }

    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
}
