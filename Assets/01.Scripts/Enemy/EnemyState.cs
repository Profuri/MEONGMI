using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    private BaseEnemy _baseEnemy;

    public BaseEnemy BaseEnemy
    {
        get
        {
            if (_baseEnemy == null)
            {
                if (_baseEnemy is BaseEnemy baseEnemy)
                {
                    this._baseEnemy = baseEnemy;
                }
                else
                {
                    Debug.Log("Can't convert BaseEnemy");
                }
            }
            return _baseEnemy;
        }
    }
    
    public EnemyState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {

    }
}
