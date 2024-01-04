using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class TutorialPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _infoText;
    public bool isOn = true;

    private void Awake()
    {
        isOn = true;
    }
    
    private void Update()
    {
        if (isOn)
        {
            _infoText.transform.rotation = Quaternion.LookRotation((Core.Define.MainCam.transform.position - _infoText.transform.position) * -1);
        }
        else
        {
            _infoText.gameObject.SetActive(false);
        }
    }
}
