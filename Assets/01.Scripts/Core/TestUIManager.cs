using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIManager : MonoSingleton<TestUIManager>
{
    public override void Init()
    {
        
    }

    public void UpgradeBase() => UpgradeManager.Instance.Upgrade(EUpgradeType.BASE);
    public void UpgradePlayer() => UpgradeManager.Instance.Upgrade(EUpgradeType.PLAYER);
    public void UpgradeTrait() => UpgradeManager.Instance.Upgrade(EUpgradeType.TRAIT);
    
    public void UpgradeFail()
    {
        // 자원이 부족합니다 UI띄어줘야 함.
    }

    public void SetUpgradeElem(EUpgradeType type, int elemNum) // 3개 띄워줘야 함
    {
        if(type == EUpgradeType.BASE)
        {
            //base 3개 띄움
        }
        else
        {
            //각 타입의 elemNum의 upgrade 를 띄움
        }
    }
}
