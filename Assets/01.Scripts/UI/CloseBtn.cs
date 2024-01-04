using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        SoundManager.Instance.PlaySFX("Close");
    }
}
