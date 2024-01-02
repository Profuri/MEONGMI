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
}
