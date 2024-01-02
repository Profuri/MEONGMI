using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _phaseText;

    public void SetDay(int day)
    {
        _dayText.text = $"Day {day}";
    }

    public void SetPhase(bool value)
    {
        if(value)
        {
            _phaseText.text = $"Defence Phase";
        }
        else
        {
            _phaseText.text = $"Prepare Phase";
        }
    }
}
