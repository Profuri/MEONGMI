using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InputControl;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    [SerializeField] private Sprite[] _sfxIcons;
    [SerializeField] private Sprite[] _bgmIcons;

    [SerializeField] private Image _sfxImage;
    [SerializeField] private Image _bgmImage;

    [SerializeField] private AudioMixerGroup _sfxGroup;
    [SerializeField] private AudioMixerGroup _bgmGroup;

    [SerializeField] private float _maxVolume = 20f;
    [SerializeField] private float _minVolume = -80f;

    private void Awake()
    {
        _inputReader.OnESCInputEvent += Remove;
    }

    public void Generate()
    {
        gameObject.SetActive(true);
        ((RectTransform)transform).DOKill();
        ((RectTransform)transform).DOScaleY(1, 0.3f).SetUpdate(true);
    }

    private void Remove()
    {
        ((RectTransform)transform).DOKill();
        ((RectTransform)transform).DOScaleY(0, 0.3f).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void OnSfxSliderValueChanged(float value)
    {
        float volume = Mathf.Lerp(_minVolume, _maxVolume, value);
        _sfxImage.sprite = _sfxIcons[volume > _minVolume ? 0 : 1];
        _sfxGroup.audioMixer.SetFloat("Volume", volume);
    }

    public void OnBgmSliderValueChanged(float value)
    {
        float volume = Mathf.Lerp(_minVolume, _maxVolume, value);
        _bgmImage.sprite = _bgmIcons[volume > _minVolume ? 0 : 1];
        _bgmGroup.audioMixer.SetFloat("Volume", volume);
    }
}
