using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelectUI : MonoBehaviour
{
    [SerializeField] public GameObject ActivePanel;
    public void Active()
    {
        ActivePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
    