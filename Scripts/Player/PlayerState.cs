using Godot;
using System;
using System.Diagnostics;

public abstract class PlayerState 
{
    // Instance Attributes:
    protected PlayerController player;

    
    // Constructor:
    public PlayerState(PlayerController controller)
    {
        Debug.Assert(controller != null);

        this.player = controller;
    }


    // Methods:
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update(float delta);
}