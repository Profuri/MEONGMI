using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Entity
{
    protected override void RegisterStates()
    {
    }

    protected override void SetInitState()
    {
    }

    public override void Init()
    {
    }

    public override void Damaged(float damage)
    {
        Debug.Log("attacked");
    }
}
