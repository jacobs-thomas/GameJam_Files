using Godot;
using System;

public interface IPlayerInteractable
{
    /**
    * Interface for interactable objects in the game.
    */


    // Methods:
    public abstract void Interact(in PlayerController playerController);
}
