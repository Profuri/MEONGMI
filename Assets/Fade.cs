using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] private float _duration= 1.5f;

    private bool _fade;
    private RectTransform _rectTrm;

    private void Awake()
    {
        _rectTrm = transform as RectTransform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            FadeOut();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        if (_fade) return;
        _fade = true;
        _rectTrm.sizeDelta = Vector2.zero;
        _rectTrm.DOSizeDelta(Vector2.one * 3000f, _duration).OnComplete(() => _fade = false);
    }

    public void FadeOut()
    {
        if (_fade) return;
        _fade = true;
        _rectTrm.sizeDelta = Vector2.one * 3000f;
        _rectTrm.DOSizeDelta(Vector2.zero, _duration).OnComplete(() => _fade = false);
    }
}
