using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    private void Awake()
    {
        GameManager.Instance.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            List<ResourceMono> resourceList = FindObjectsOfType<ResourceMono>().ToList();
            resourceList.ForEach(r => r.GetResource());
        }
    }

    public override void Init()
    {
        ResManager.Instance.Init();
    }
}
