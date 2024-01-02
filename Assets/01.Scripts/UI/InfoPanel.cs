using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Tooltip")]
    [SerializeField] private Tooltip _tooltip;
    [SerializeField] private string _tooltipText = "tooltip";
    [SerializeField] private float _tooltipWidth = 75f;
    [SerializeField] private float _tooltipYPos = 10f;
     
    [Header("Icon")]
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private Image _icon;

    private void Awake()
    {
        _tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.SetActive(true, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(_iconSprite != null && _icon != null)
        {
            _icon.sprite = _iconSprite;
        }

        if (_tooltip != null)
        {
            _tooltip.SetText(_tooltipText);
            _tooltip.SetWidth(_tooltipWidth);
            _tooltip.SetYPos(_tooltipYPos);
        }
    }
#endif
}
