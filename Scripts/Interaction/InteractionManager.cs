using Godot;
using System;
using System.Collections.Generic;

public partial class InteractionManager : Area3D
{
    // Class Attributes:
    private const MouseButton INTERACTION_KEY = MouseButton.Left;


    // Instance Attributes:
    [Export] public PlayerController PlayerController { get; private set; }

    private IPlayerInteractable playerInteractable = null;


    // Methods:
    public override void _Ready()
    {
        base._Ready();

        // Connect the signals to the methods.
        Connect("body_entered", new Callable(this, MethodName.OnBodyEnter));
        Connect("body_exited", new Callable(this, MethodName.OnBodyExit));
    }

    private void OnBodyEnter(Node body)
    {
        if (body is Node3D node && node is IPlayerInteractable interactable)
        {
            GD.Print($"Object enter: {node.Name}");
            playerInteractable = interactable;
        }
    }

    private void OnBodyExit(Node body)
    {
        if (body is Node3D node && node is IPlayerInteractable interactable)
        {
            GD.Print($"Object exit: {node.Name}");
            interactable = null;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);


        if (playerInteractable == null || !Input.IsMouseButtonPressed(INTERACTION_KEY)) { return; }

        GD.Print($"Interacting with: {playerInteractable.ToString()}");
        playerInteractable.Interact(PlayerController);
    }
}
