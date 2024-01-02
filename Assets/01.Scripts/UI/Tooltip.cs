using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _image;
    private RectTransform _rectTransform;

    public void SetText(string text) => _text.SetText(text);

    public void SetWidth(float width)
    {
        if(_rectTransform == null)
        {
            _rectTransform = transform as RectTransform;
        }

        Rect rt = _rectTransform.rect;
        _rectTransform.sizeDelta = new Vector2(width, rt.height);
    }

    public void SetYPos(float value)
    {
        if (_rectTransform == null)
        {
            _rectTransform = transform as RectTransform;
        }

        Vector2 pos = _rectTransform.anchoredPosition;
        pos.y = value;
        _rectTransform.anchoredPosition = pos;
    }

    public void SetActive(bool value, Vector3 position = default)
    {
        if(value)
        {
            gameObject.SetActive(true);
            _image.DOFade(1f, 0.2f);
            if (position != default)
            {
                _rectTransform.position = position;
            }
        }
        else
        {
            _image.DOFade(0f, 0.05f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}
