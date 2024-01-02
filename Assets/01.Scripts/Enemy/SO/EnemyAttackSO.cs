using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/Attack")]
public class EnemyAttackSO : ScriptableObject
{
    public EnemyType enemyType;
    public int damage;
    
    public float attackDelay;

    public float detectRange;
    public float attackRange;
}
