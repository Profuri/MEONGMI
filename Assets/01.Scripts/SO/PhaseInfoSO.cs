using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Phase")]
public class PhaseInfoSO : ScriptableObject
{
    public EnemyListSO enemyListSO;
    
    public int appearMaxEnemyCnt;
    public int appearOnceMinEnemyCnt;
    public int appearOnceMaxEnemyCnt;
    public int appearDelay;

    public int resMinSpawnCnt;
    public int resMaxSpawnCnt;
    public float resMinSpawnTime;
    public float resMaxSpawnTime;

    public int GetSpawnCnt() => Random.Range(resMinSpawnCnt, resMaxSpawnCnt + 1);
    public float GetSpawnTime() => Random.Range(resMinSpawnTime, resMaxSpawnTime);
}
