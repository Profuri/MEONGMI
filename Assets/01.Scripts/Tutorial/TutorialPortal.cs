using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TutorialPortal : MonoBehaviour
{
    [SerializeField] private float _interactRadius = 3f;
    [SerializeField] private LayerMask _playerLayer;
    private Collider _collider;

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _interactRadius, _playerLayer);

        if (cols.Length > 0)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
