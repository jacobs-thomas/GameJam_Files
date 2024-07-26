using Godot;
using Godot.Collections;
using System;


public partial class PlayerController : CharacterBody3D
{
    // Class Attributes:
    private const Key JumpKey = Key.Space;
    private const string JumpAction = "jump";


    // Instance Attributes:
    [Export] public float WalkSpeed { get; set; } = 4.0f;
    [Export] public float LookSpeed { get; set; } = 1.0f;
    [Export] public float JumpForce { get; set; } = 10.0f;

    [Export] public HandController handController = null;
    [Export] private MeshInstance3D meshInstance = null;
    [Export] private CollisionShape3D collisionShape = null;
    [Export] private CameraController cameraController = null;

    private readonly float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private float previousMousePositionX = 0.0f;
    private Vector3 velocity = Vector3.Zero;
    private bool isMimic = false;
    private Shape3D playerShape = null;
    private Mesh playerMesh = null;
    private const float HOLD_DURATION = 2.0f;
    private float inputHeldTime = 0.0f;
    private bool isInputHeld = false;

    private readonly InputAxis depthAxis = new InputAxis("depth", Key.W, Key.S);
    private readonly InputAxis horizontalAxis = new InputAxis("horizontal", Key.D, Key.A);


    // Properties:
    public CameraController CameraController => cameraController;


    // Methods:
    public override void _Ready()
	{
        /**
		 * Called when the node is added to the scene.
		 */


        InputAxis.AddAction(JumpAction, JumpKey);
        previousMousePositionX = GetViewport().GetMousePosition().X;

        if (collisionShape != null) { playerShape = collisionShape.Shape; }
        if (meshInstance != null) {  playerMesh =  meshInstance.Mesh; }
    }

	public override void _PhysicsProcess(double delta)
	{
        /**
		 * Called every physics frame (fixed frame rate).
		 * This method updates the character's velocity based on input, gravity, and jumping.
		 * It ensures frame rate-independent movement by using the delta parameter.
		 *
		 * @param delta The time elapsed (in seconds) since the previous physics frame.
		 */


        // Calculate the new velocity based on input and current vertical velocity.
        Vector3 mNewVelocity = Velocity.Y * Vector3.Up + ((-GlobalTransform.Basis.Z * depthAxis.Value + GlobalTransform.Basis.X * horizontalAxis.Value) * WalkSpeed);

        // Apply gravity if the player is not on the floor.
        if (!IsOnFloor()) { mNewVelocity.Y -= gravity * (float)delta; }

        // Handle jumping if the player is on the floor.
        else if (Input.IsKeyPressed(Key.Space)) { mNewVelocity.Y = JumpForce; }

        // Horizontal Rotation: 
        float currentMousePositionX = GetViewport().GetMousePosition().X;
        RotateY((previousMousePositionX - currentMousePositionX) * (float)delta * LookSpeed);

        // Update the previous mouse position.
        previousMousePositionX = currentMousePositionX;

        // Set the new velocity.
        Velocity = mNewVelocity;


        // Everything after here needs optimisation...

        // Mimic: 
        if (Input.IsMouseButtonPressed(MouseButton.Right) && cameraController != null && cameraController.IsLookingAtNode<MimicItem>(new Array<Rid> { GetRid() }, out MimicItem mimicItem))
        {
            inputHeldTime += (float)delta;

            if(inputHeldTime >= HOLD_DURATION)
            {
                Mimic(mimicItem);
            }
        }
        else
        {
            inputHeldTime = 0;
        }

        // Exit mimic mode.
        if (Input.IsKeyPressed(Key.Q) && meshInstance != null && collisionShape != null)
        {
            meshInstance.Mesh = playerMesh;
            collisionShape.Shape = playerShape;
        }


        // Interaction: 
        if (Input.IsMouseButtonPressed(MouseButton.Left) && cameraController != null && cameraController.IsLookingAtNode<CollectableItem>(new Array<Rid> { GetRid() }, out CollectableItem collectableItem))
        {
            handController?.Hold(collectableItem);
        }
        if (Input.IsKeyPressed(Key.E) && handController != null)
        {
            handController.Drop();
        }

        MoveAndSlide();
    }

    public void Mimic(MimicItem mimicItem)
    {
        meshInstance.Mesh = mimicItem.Mesh;
        collisionShape.Shape = mimicItem.Shape;
    }
}
