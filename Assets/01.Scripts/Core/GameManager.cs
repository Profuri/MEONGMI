using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    public Transform PlayerTrm { get; set; }
    public Transform BaseTrm { get; set; }
    
    private void Awake()
    {
        GameManager.Instance.Init();
    }

    public override void Init()
    {
        PlayerTrm = GameObject.Find("Player").transform;
        BaseTrm = GameObject.Find("Base").transform;
        ResManager.Instance.Init();
        UIManager.Instance.Init();
    }
}
