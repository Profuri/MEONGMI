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

    private UpgradeCard _upgradeCard;
    private UpgradeElemInfoSO info;
    private bool _isRolling = false;

    private void Awake()
    {
        _title.SetText(string.Empty);
        Init();
    }

    private void Init()
    {
        _upgradeCard = transform.GetComponent<UpgradeCard>();
        info = _upgradeCard.Info;
    }

    public void RollImage()
    {
        if (_isRolling || info == null) return;
        _isRolling = true;

        Image[] imageList = ShuffleArray(_imageListTrm.GetComponentsInChildren<Image>());

        _imageListTrm.DOAnchorPosY((_imageListTrm.childCount - 1) * 235f, 1.5f)
        .OnComplete(() =>
        {
            imageList[0].sprite = info.Image;
            _imageListTrm.anchoredPosition = Vector2.zero;
            _title.SetText(info.Description);

            _isRolling = false;
        });
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
