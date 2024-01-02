using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/List")]
public class EnemyListSO : ScriptableObject
{
    public List<BaseEnemy> enemyList = new List<BaseEnemy>();

    public BaseEnemy GetRandomEnemy()
    {
        int randomValue = Random.Range(0,enemyList.Count);
        return enemyList[randomValue];
    }
}
