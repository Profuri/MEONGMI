using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class StartBtn : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private string _originText;
    [SerializeField] private float _plusFontSize = 7f;
    private TextMeshProUGUI _text;
    private TextAnimator_TMP _textAnimator;

    private void Awake()
    {
        Transform textTrm = transform.Find("Text");
        _textAnimator = textTrm.GetComponent<TextAnimator_TMP>();
        _text = textTrm.GetComponent<TextMeshProUGUI>();
        _originText = _text.text;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.fontSize += _plusFontSize;
        _textAnimator.SetText($"<pend>{_originText}<pend>");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _text.fontSize -= _plusFontSize;
        _textAnimator.SetText($"{_originText}");
    }
}
