using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerIn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PhaseManager.Instance.PhaseType == PhaseType.Rest)
            {
                UIManager.Instance.ShowUpgradeUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideUpgradeUI();
        }
    }

}
