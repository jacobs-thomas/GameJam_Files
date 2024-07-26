using Godot;
using System;

public interface IPlayerInteractable
{
    /**
    * Interface for interactable objects in the game.
    */


    // Methods:
    public virtual void Interact(in PlayerController playerController)
    {
        /**
        * Method to be called when the player interacts with this object.
        * @param player The player interacting with the object.
        */


    }
}
