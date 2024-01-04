using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatureInfoPanel : InfoPanel
{
    //[SerializeField] private bool _isActive;
    public Sprite emptySprite;
    private ETraitUpgradeElement myType;
    public ETraitUpgradeElement TraitType 
    { 
        get { return myType; } 
        set 
        {
            if (myType != value)
                ChangeSO(value);
        }
    }

    private void ChangeSO(ETraitUpgradeElement type)
    {
        if (type != ETraitUpgradeElement.NONE && type != ETraitUpgradeElement.END)
        {
            TraitUpgradeElemSO elem = UpgradeManager.Instance.TraitElemInfos.Find((trait) => trait.Type == type);
            _tooltipText = elem.Description;
            _iconSprite = elem.Image;
            _icon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _tooltipText = "활성화된 특성이 없음";
            _icon.color = new Color(1,1,1,0);
        }
        _icon.sprite = _iconSprite;
    }
}
