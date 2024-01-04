using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TutorialBase : MonoBehaviour
{
    [SerializeField] private Transform _playerTrm;

    private void Update()
    {
        if (_playerTrm != null)
        {
            if (Vector3.Distance(transform.position, _playerTrm.position) > 10f)
            {
                _playerTrm.position = Vector3.zero;
            }
        }
    }
}
