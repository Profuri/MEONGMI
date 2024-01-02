using System;

public class PlayerShotState : PlayerState
{
    public PlayerShotState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    }
}