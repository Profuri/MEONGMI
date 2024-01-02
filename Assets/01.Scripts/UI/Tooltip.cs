using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
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
        gameObject.SetActive(value);
        if(value)
        {
            if(position != default)
            {
                _rectTransform.position = position;
            }
        }
    }
}
