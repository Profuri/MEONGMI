using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoSingleton<TutorialSpawner>
{
    [SerializeField] private EnemyListSO _enemyListSO;
    public override void Init()
    {
        
    }
    
    public List<BaseEnemy> SpawnEnemies()
    {
        List<BaseEnemy> enemyList = new List<BaseEnemy>();
        foreach (BaseEnemy baseEnemy in _enemyListSO.enemyList)
        {
            Vector3 spawnPos = transform.position;
            BaseEnemy enemy = PoolManager.Instance.Pop($"{baseEnemy.gameObject.name}") as BaseEnemy;
            enemy.Init();
            enemy.gameObject.SetActive(true);
            enemy.SetPosition(spawnPos);
            enemyList.Add(enemy);
        }

        return enemyList;
    }
}
