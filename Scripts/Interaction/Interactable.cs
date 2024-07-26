using Godot;
using System;

public interface IPlayerInteractable
{
    // Methods:
    public abstract void Interact(in PlayerController playerController);
}
