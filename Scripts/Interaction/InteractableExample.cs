using Godot;
using System;

public partial class InteractableExample : StaticBody3D, IPlayerInteractable
{
    // Methods:
    public void Interact(in PlayerController playerController)
    {
        GD.Print($"Interaction with {this.ToString()}");
    }
}
