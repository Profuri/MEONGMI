using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class ResourceChecker : MonoBehaviour
{
    [SerializeField] private float _detectRadius = 3f;
    [SerializeField] private LayerMask _layerMask;
    
    private void Update()
    {
        Vector3 originPos = transform.position;
        float radius = _detectRadius;
        Collider[] cols = Physics.OverlapSphere(originPos, radius, _layerMask);

        if (cols.Length > 0)
        {
            foreach (Collider col in cols)
            {
                if (col.TryGetComponent(out IGatherable gatherable))
                {
                    ResManager.Instance.AddResource(gatherable.GetGatheringAmount());
                }
            }
        }
    }
}
