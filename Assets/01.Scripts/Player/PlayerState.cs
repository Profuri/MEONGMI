using System;
using UnityEngine;

public class PlayerState : State
{
    protected PlayerController _player => _owner as PlayerController;
    
    public PlayerState(StateMachine stateMachine, Entity owner, Enum type) : base(stateMachine, owner, type)
    {
    
    }
}