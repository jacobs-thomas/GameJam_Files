using Godot;

#nullable enable

public partial class Metamorph : Node3D, IPlayerInteractable
{
    /**
     * The `Metamorph` class is a `Node3D` that manages mesh and shape attributes.
     * It provides functionality to automatically assign default values and search for specific
     * nodes in its hierarchy to update these attributes.
     */


    // Class Attributes:
    private static readonly Mesh DEFAULT_MESH = new SphereMesh();
    private static readonly Shape3D DEFAULT_SHAPE = new SphereShape3D();


    // Instance Attributes:
    [Export] public string Tag { get; set; } = "DEFAULT_TAG";

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
        if (TryGetNode<MeshInstance3D>(this, out MeshInstance3D meshInstnace))
        {
            mesh = meshInstnace.Mesh;
        }

        // Try to find and set a CollisionShape3D child node's shape.
        if (TryGetNode<CollisionShape3D>(this, out CollisionShape3D collisionShape))
        {
            shape = collisionShape.Shape;
        }
    }

    private bool TryGetNode<T>(Node node, out T result) where T : Node
    {
        /**
         * Searches recursively for a node of type T in the given node's children.
         * @param node The root node to start the search from.
         * @param result Outputs the found node of type T, or null if not found.
         * @return True if a node of type T was found, otherwise false.
         */


        result = null;

        // Check if the current node is of type T.
        if (node is T typedNode)
        {
            result = typedNode;
            return true;
        }

        // Recursively search through each child node.
        foreach (Node child in node.GetChildren())
        {
            if (TryGetNode(child, out result))
            {
                return true;
            }
        }

        return false;
    }
}
