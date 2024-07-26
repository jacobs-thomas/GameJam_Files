using Godot;
using Godot.Collections;
using System;

public partial class CameraController : Camera3D
{
    // Class Attributes:
    private const float RAYCAST_LENGTH = 100.0f;


    // Instance Attributes:
    [Export] public float RotationSpeed = 0.1f; // Speed of rotation
    [Export] public float MaxPitch = 60.0f; // Maximum pitch in degrees
    [Export] public float MinPitch = -60.0f; // Minimum pitch in degrees

    private float previousMousePositionY;
    private float currentPitch = 0.0f;


    // Methods:
    private Dictionary Raycast(Array<Rid> exclude)
    {

        // Get a state of the current physics simulation.
        PhysicsDirectSpaceState3D state = GetWorld3D().DirectSpaceState;

        // Generate a raycast query for the physics state, excluding the player from the raycast.
        PhysicsRayQueryParameters3D raycastQuery = PhysicsRayQueryParameters3D.Create(GlobalTransform.Origin, -GlobalTransform.Basis.Z * RAYCAST_LENGTH);
        raycastQuery.Exclude = exclude;

        // Collect the result in a dictionary.
        return state.IntersectRay(raycastQuery);
    }

    public bool IsLookingAtNode<T>(Array<Rid> exclude, out T interactable) where T : Node
    {
        // Collect the result in a dictionary.
        Dictionary result = Raycast(exclude);

        Node node = (Node)result["collider"];
        interactable = node is T? (T)node : null;

        return interactable != null;
    }

    public bool IsLookingAtInteractable(Array<Rid> exclude, out IPlayerInteractable interactable)
    {
        interactable = null;

        Dictionary result = Raycast(exclude);

        // If the dictionary is not empty, the raycast has hit an object.
        if (result.Count <= 0) { return false; }

        Node physicsBody = (Node)result["collider"];

        interactable = (physicsBody is IPlayerInteractable) ? (IPlayerInteractable)physicsBody : null;

        return interactable != null;
    }

    public override void _Ready()
    {
        // Initialize previousMousePositionY to the current mouse Y position.
        previousMousePositionY = GetViewport().GetMousePosition().Y;
    }

    public override void _Process(double delta)
    {
        /**
         * Updates the camera's pitch based on mouse movement.
         * 
         * This method is called every frame to process user input and adjust the camera's rotation.
         * It calculates the change in the mouse's Y position, updates the pitch rotation of the camera,
         * and clamps it within a specified range to prevent excessive rotation.
         * 
         * @param delta The time elapsed since the last frame, used to scale the rotation speed.
         */


        // Get current mouse position
        float currentMousePositionY = GetViewport().GetMousePosition().Y;

        // Calculate the change in mouse position.
        float mouseDeltaY = previousMousePositionY - currentMousePositionY;

        // Update previousMousePositionY for the next frame.
        previousMousePositionY = currentMousePositionY;

        // Calculate the new pitch based on mouse movement.
        currentPitch += mouseDeltaY * RotationSpeed * (float)delta;

        // Clamp the pitch within the specified range.
        currentPitch = Mathf.Clamp(currentPitch, MinPitch, MaxPitch);

        // Apply the clamped rotation to the camera.
        Basis cameraBasis = Basis.Identity.Rotated(Vector3.Right, Mathf.DegToRad(currentPitch));
        Transform = new Transform3D(cameraBasis, Transform.Origin);
    }
}
