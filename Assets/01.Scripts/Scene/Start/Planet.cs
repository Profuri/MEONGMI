using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 8f;
    
    private void Update()
    {
        transform.Rotate(transform.up * _rotateSpeed * Time.deltaTime);
    }
}
