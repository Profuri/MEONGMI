using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeCard : UpgradeCard
{
    public override void OnClick()
    {
        PlayerUpgradeElemSO so = Info as PlayerUpgradeElemSO;
        UpgradeManager.Instance.ApplyUpgradePlayer(so.Type);
        Debug.Log(so.Type.ToString());
        base.OnClick();
    }
}
