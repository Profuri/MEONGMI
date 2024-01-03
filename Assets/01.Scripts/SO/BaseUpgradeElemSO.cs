using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/UpgradeInfo/Base")]
public class BaseUpgradeElemSO : UpgradeElemInfoSO
{
    public EBaseUpgradeElement Type;
    public Sprite NeedResource;
    
    [Header("Cost")]
    public int BaseNeedCost;
    public int AddCost;

    [Header("Stat")]
    public int BaseValue;
    public int AddValue;

}
