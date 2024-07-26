using Godot;
using System;

public partial class Item : RigidBody3D, IPlayerInteractable
{
    // Properties:
    [Export] public string ItemName { get; set; }
    [Export] public string Description { get; set; }
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

    [Export] public CollisionShape3D CollisionShape { private set; get; }

    // Methods:
    public void Interact(in PlayerController playerController)
    {
        //playerController.handController.Hold(this);
        GD.Print("Item interaction invoked");
    }
}
