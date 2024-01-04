using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UpgradeSelectButton : MonoBehaviour
{
    [SerializeField] EUpgradeType Type;
    [SerializeField] Ease inEase;
    [SerializeField] Ease Outease;

    [SerializeField] private bool _payThis;
    [SerializeField] private int _payment;
    
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

        for (int i = 0; i < images.Length; i++)
        {
            images[i].DOFade(0, 0);
        }
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].DOFade(0, 0);
        }
    }

    public void Show()
    {
        for (int i = 0; i < images.Length; i++) images[i].DOKill();
        for (int i = 0; i < texts.Length; i++) texts[i].DOKill();

        Sequence seq = DOTween.Sequence();
        for(int i = 0; i < images.Length; i++)
        {
            seq.Insert(0, images[i].DOFade(1, 0.6f).SetEase(inEase));
        }
        for(int i = 0; i < texts.Length; i++)
        {
            seq.Insert(0, texts[i].DOFade(1, 0.6f).SetEase(inEase));
        }
        seq.OnComplete(() => button.interactable = true);
    }

    public void HIde()
    {
        button.interactable = false;
        for (int i = 0; i < images.Length; i++) images[i].DOKill();
        for (int i = 0; i < texts.Length; i++) texts[i].DOKill();
           

        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < images.Length; i++)
        {
            seq.Insert(0, images[i].DOFade(0, 0.6f).SetEase(Outease));
        }
        for (int i = 0; i < texts.Length; i++)
        {
            seq.Insert(0, texts[i].DOFade(0, 0.6f).SetEase(Outease));
        }
    }

    public void OnClick()
    {
        switch (Type)
        {
            case EUpgradeType.BASE:
                if (_payThis)
                {
                    if (!ResManager.Instance.UseResource(_payment))
                    {
                        return;
                    }
                }
                UIManager.Instance.ChangeUI("BaseUpgradePanel");
                break;
            case EUpgradeType.PLAYER:
                if (_payThis)
                {
                    if (!ResManager.Instance.UseResource(_payment))
                    {
                        return;
                    }          
                }
                UIManager.Instance.ChangeUI("PlayerLevelUpPanel");
                break;
            case EUpgradeType.TRAIT:
                if (_payThis)
                {
                    if (!ResManager.Instance.UseResource(_payment))
                    {
                        return;
                    }          
                }
                UIManager.Instance.ChangeUI("PlayerFeatureChoicePanel");
                break;
        }

        
    }
}