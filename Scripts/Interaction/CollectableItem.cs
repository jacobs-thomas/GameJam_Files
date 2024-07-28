using Godot;
using System;
using System.Diagnostics;

public partial class CollectableItem : RigidBody3D, IPlayerInteractable
{
    /**
    * Represents an item in the game that can be picked up by the player.
    */


    // Properties:
    [Export] private CollisionShape3D CollisionShape { set; get; }
    [Export] public string ItemName { get; private set; }
    [Export] public string Description { get; private set; }
    [Export] public bool IsPhysicsEnabled 
    {
        set
        {
            if(CollisionShape == null) { return; }
            CollisionShape.Disabled = !value;
            Freeze = !value;
        }

        get
        {
            return CollisionShape == null ? false : !CollisionShape.Disabled;
        }
    }

    // Methods:
    public void Interact(in PlayerController playerController)
    {
        Debug.Assert(playerController != null);

        playerController.handController.Hold(this);
    }
}
