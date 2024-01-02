using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatChoicePanel : ChoicePanel
{
    [SerializeField] private RectTransform _imageListTrm;
    [SerializeField] private List<Sprite> _statIconImages;
    [SerializeField] private TextMeshProUGUI _title;

    private bool _isRolling = false;

    private void Awake()
    {
        _title.SetText(string.Empty);
    }

    private void Update()
    {
        //Debug
        if(Input.GetKeyDown(KeyCode.K))
        {
            RollImage();
        }
    }

    public void RollImage()
    {
        if (_isRolling) return;
        _isRolling = true;

        Image[] imageList = _imageListTrm.GetComponentsInChildren<Image>();
        for(int i = 0; i < _imageListTrm.childCount; i++)
        {
            Image img = imageList[i];
            int rand = Random.Range(0, _statIconImages.Count - 1);
            img.sprite = _statIconImages[rand];
        }

        _imageListTrm.DOAnchorPosY((_imageListTrm.childCount - 1) * 235f, 1.5f)
        .OnComplete(() =>
        {
            imageList[0].sprite = imageList[imageList.Length - 1].sprite;
            _imageListTrm.anchoredPosition = Vector2.zero;
            _title.SetText(imageList[0].sprite.name);

            _isRolling = false;
        });
    }
}
