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
        // �ڿ��� �����մϴ� UI������ ��.
    }

    public void SetUpgradeElem(EUpgradeType type, int elemNum) // 3�� ������ ��
    {
        if(type == EUpgradeType.BASE)
        {
            //base 3�� ���
        }
        else
        {
            //�� Ÿ���� elemNum�� upgrade �� ���
        }
    }
}
