using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolingListSO _poolingListSO;
    
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
        
        PoolManager.Instance = new PoolManager(this.transform);
        foreach (var pair in _poolingListSO.pairs)
        {
            PoolManager.Instance.CreatePool(pair.prefab,pair.count);
        }
        
        
        ResManager.Instance.Init();
        EnemySpawner.Instance.Init();
        EnemySpawner.Instance.StartPhase(0);


    }
}
