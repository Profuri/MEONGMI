using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;
using TMPro;

public class InformationText : MonoBehaviour
{
    private TMP_Text _text;
    private TextAnimator_TMP _textAnimator;
    private Transform _playerTrm;
    
    private void Awake()
    {
        _playerTrm = GameObject.Find("Player").transform;
        _text = GetComponent<TMP_Text>();
        _textAnimator = GetComponent<TextAnimator_TMP>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation((CameraManager.Instance.MainCam.transform.position - transform.position) * -1);
    }
    public void SetText(string text)
    {
        _textAnimator.SetText(text);
    }
}
