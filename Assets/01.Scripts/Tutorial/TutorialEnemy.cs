using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialEnemy : MonoBehaviour
{
    [SerializeField] private string _text;
    [SerializeField] private InformationText _informationText;
    [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0f);
    private bool _isOn = false;

    [SerializeField] private float _detectRadius = 5f;
    
    private void Awake()
    {
        _isOn = false;
        _informationText = Instantiate<InformationText>(_informationText);
        _informationText.gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector3 originPos = transform.position;
        Collider[] cols = Physics.OverlapSphere(originPos, _detectRadius, 1 << LayerMask.NameToLayer("Player"));
        
        if (cols.Length > 0  && _isOn == false)
        {
            _isOn = true;
            _informationText.gameObject.SetActive(true);
            _informationText.transform.position = transform.position + _offset;
            _informationText.SetText(_text);
        }
        else if(cols.Length == 0)
        {
            _isOn = false;
            _informationText.gameObject.SetActive(false);
        }
    }
}
