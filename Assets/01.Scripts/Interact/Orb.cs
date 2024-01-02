using UnityEngine;

public class Orb : Interactable
{
    public override void OnInteract(Entity entity)
    {
        entity.StateMachine.ChangeState(PlayerStateType.Gather);
        
    }
}