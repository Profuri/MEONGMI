using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    private TextMeshProUGUI description;
    private Image image;

    private void Awake()
    {
        description = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        image = transform.Find("Image").GetComponent<Image>();
    }

    public void Setting(UpgradeElemInfoSO so)
    {
        description.text = so.Description;
        image.sprite = so.Image;
        gameObject.name = so.name;
    }
}
