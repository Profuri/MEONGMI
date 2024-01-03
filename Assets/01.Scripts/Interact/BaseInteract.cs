using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteract : Interactable
{
    public override void OnInteract(Entity entity)
    {

        entity.StateMachine.ChangeState(PlayerStateType.Idle);
        TestUIManager.Instance.UpgradeRootPanelOn();
    }
}
