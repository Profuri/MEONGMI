using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Tooltip")]
    [SerializeField] protected Tooltip _tooltip;
    [SerializeField] protected string _tooltipText = "tooltip";
    [SerializeField] protected float _tooltipWidth = 75f;
    [SerializeField] protected float _tooltipYPos = 10f;
    [SerializeField] protected bool _tooltipPosChange = true;
     
    [Header("Icon")]
    [SerializeField] protected Sprite _iconSprite;
    [SerializeField] protected Image _icon;

    private void Awake()
    {
        _tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.SetActive(true, _tooltipPosChange ? eventData.position : default);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.SetActive(false);
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
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
