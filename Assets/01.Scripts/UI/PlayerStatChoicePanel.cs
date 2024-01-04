using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStatChoicePanel : MonoBehaviour
{
    [SerializeField] private RectTransform _imageListTrm;
    //[SerializeField] private UpgradeContainer _upgradeContainer;
    [SerializeField] private Sprite[] _statIconImages;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;
    //[SerializeField] private EUpgradeType _upgradeType;
    [SerializeField] private GameObject _backglow;
    [SerializeField] private Image Frame_Focus;

    [SerializeField] private Button _button;

    [SerializeField] private UpgradeCard _upgradeCard;
    private bool _isRolling = false;

    private void Awake()
    {
        _title.SetText(string.Empty);
        _description.SetText(string.Empty);
        Init();
    }

    private void Start()
    {
        RollImage();
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

        Image[] imageList = _imageListTrm.GetComponentsInChildren<Image>();
        _statIconImages = ShuffleArray(_statIconImages);
        //imageList = ShuffleArray(imageList);
        for (int i = 0; i < imageList.Length; i++)
        {
            Image img = imageList[i];
            img.sprite = _statIconImages[i];
        }
        imageList[imageList.Length - 1].sprite = _upgradeCard.Info.Image;

        _imageListTrm.DOAnchorPosY((imageList.Length - 1) * 235f, 1.5f)
        .OnComplete(() =>
        {
            imageList[0].sprite = imageList[imageList.Length - 1].sprite;
            EndRolling(imageList[0]);
        });
    }

    private void EndRolling(Image imageContainer)
    {
        //imageContainer.sprite = _upgradeCard.Info.Image;
        _imageListTrm.anchoredPosition = Vector2.zero;
        _title.SetText(_upgradeCard.Info.Name);
        _description.SetText(_upgradeCard.Info.Description);
        _button.enabled = true;
        _backglow.SetActive(true);
        //_isRolling = false;
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
