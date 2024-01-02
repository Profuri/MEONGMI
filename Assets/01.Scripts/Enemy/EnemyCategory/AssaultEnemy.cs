using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultEnemy : BaseEnemy
{
    public override void Awake()
    {
        base.Awake();
        EnemyType = EnemyType.Assault;
        //얘는 타워만 공격하는 놈임.
        //This is only attacking base.
    }
}
