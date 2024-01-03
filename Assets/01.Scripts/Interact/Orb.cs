using UnityEngine;

public class Orb : Interactable
{
    public override void OnInteract(Entity entity)
    {
        PlayerController player = entity as PlayerController;
        if(player != null)
        {
            entity.StateMachine.ChangeState(PlayerStateType.Gather);
        }
    }
}