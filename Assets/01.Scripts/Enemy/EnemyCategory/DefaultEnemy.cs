using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemy : BaseEnemy
{
    public override void Awake()
    {
        base.Awake();
        
        EnemyType = EnemyType.Default;
    }
}
