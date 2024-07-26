using Godot;
using System;
using System.Diagnostics;

public partial class VisualPhysicsObject : Node3D
{
    /**
     * VisualPhysicsObject is a Node3D that combines a MeshInstance3D and a CollisionShape3D.
     * It facilitates the management of both the visual and physical aspects of an object.
     */


    // Instance Attributes:
    [Export] private MeshInstance3D meshInstance = null;
	[Export] private CollisionShape3D collisionShape = null;


	// Properties:
	public bool IsEnabled
	{
        /**
         * Property to enable or disable the visual and collision components.
         * When set to true, both the MeshInstance and CollisionShape will be enabled.
         * When set to false, both will be disabled.
         */


        set
        {
            if(meshInstance != null) { meshInstance.Visible = value; }

            if(collisionShape != null)
            {
                collisionShape.Visible = value;
                collisionShape.Disabled = !value;
            }
        }
	}


    // Constructors:
    public VisualPhysicsObject(MeshInstance3D meshInstance, CollisionShape3D collisionShape)
    {
        Debug.Assert(meshInstance != null && collisionShape != null);


        this.meshInstance = meshInstance;
        this.collisionShape = collisionShape;

        AddChild(meshInstance);
        AddChild(collisionShape);
    }

    public VisualPhysicsObject()
    {
        meshInstance = new MeshInstance3D();
        collisionShape = new CollisionShape3D();

        AddChild(meshInstance);
        AddChild(collisionShape);
    }


	// Methods:
	public override void _EnterTree()
	{
        /**
        * Called when the node enters the scene tree for the first time.
        * Ensures that the MeshInstance3D and CollisionShape3D are properly initialized and added as children.
        */


        base._EnterTree();

        if (meshInstance == null)
        {
            meshInstance = new MeshInstance3D();
            AddChild(meshInstance);
        }

        if (collisionShape == null)
        {
            collisionShape = new CollisionShape3D();
            AddChild(collisionShape);
        }
    }

	public void Enable()
	{
        /**
        * Enables the visual and collision components of the object.
        */


        IsEnabled = true;
    }

    public void Disable()
	{
        /**
        * Disables the visual and collision components of the object.
        */


        IsEnabled = false;
    }
}
