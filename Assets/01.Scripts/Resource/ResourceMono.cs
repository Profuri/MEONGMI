using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMono : MonoBehaviour
{
    [SerializeField] private int _curResCnt;

    public void GetResource()
    {
        if (ResManager.Instance.AddResource(_curResCnt) == false)
        {
            Debug.Log("Can't add more resource!! ");
        }
    }
}
