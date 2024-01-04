using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradeSelectButton : MonoBehaviour
{
    [SerializeField] EUpgradeType Type;
    private Button button;
    private Image[] images;
    private TextMeshProUGUI[] texts;

    private void Awake()
    {
        button = GetComponent<Button>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void Show()
    {
        
        Sequence seq = DOTween.Sequence();
        for(int i = 0; i < images.Length; i++)
        {
            images[i].DOKill();
            seq.Insert(0, images[i].DOFade(1, 1f));
        }
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].DOKill();
            seq.Insert(0, texts[i].DOFade(1, 1f));
        }
        seq.Play();
    }

    public void HIde()
    {
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOKill();
            seq.Insert(0, images[i].DOFade(0, 1f));
        }
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOKill();
            seq.Insert(0, texts[i].DOFade(0, 1f));
        }
    }

    public void OnClick()
    {
        switch (Type)
        {
            case EUpgradeType.BASE:
                TestUIManager.Instance.BaseUpgradePanel();
                break;
            case EUpgradeType.PLAYER:
                TestUIManager.Instance.UpgradePlayer();
                break;
            case EUpgradeType.TRAIT:
                TestUIManager.Instance.UpgradeTrait();
                break;
        }
    }
}