using Godot;

#nullable enable

public partial class MimicItem : Node3D, IPlayerInteractable
{
    /**
     * Class representing an item that allows the player to mimic its appearance or abilities.
     */


    // Class Attributes:
    private static readonly Mesh DEFAULT_MESH = new SphereMesh();
    private static readonly Shape3D DEFAULT_SHAPE = new SphereShape3D();


    // Instance Attributes:
    [Export] private MeshInstance3D meshInstance = null;
    [Export] private CollisionShape3D collisionShape = null;

    [Export] private Mesh mesh = DEFAULT_MESH;
    [Export] private Shape3D shape = DEFAULT_SHAPE;


    // Properties:
    public Mesh Mesh => mesh;
    public Shape3D Shape => shape;


    // Methods:
    public void Interact(in PlayerController playerController)
    {
        playerController.Mimic(this);
    }

    public override void _Ready()
    {
        /**
         * Called when the node is added to the scene.
         * This method initializes the mesh and shape with default values,
         * and attempts to find and set these attributes from child nodes if they exist.
         */


        base._Ready();

        // Set default values for mesh and shape.
        mesh = DEFAULT_MESH;
        shape = DEFAULT_SHAPE;

        // Try to find and set a MeshInstance3D child node's mesh
        if(meshInstance != null)
        {
            mesh = meshInstance.Mesh;
        }

        // Try to find and set a CollisionShape3D child node's shape.
        if (collisionShape != null)
        {
            shape = collisionShape.Shape;
        }
    }
}
