using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSelectUI : MonoBehaviour
{
    private TextMeshProUGUI needMoneyText;

    public void ShowMoneyText(int money)
    {
        needMoneyText.text = money.ToString();
    }

    
    
        
}
    