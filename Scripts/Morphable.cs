using Godot;
using System;

public partial class Morphable : RigidBody3D
{
	// Class Attributes:
	public const uint LAYER_MASK = 1000;

	// Instance Attributes:
	[Export] public NodePath mCollisionShapePath = "";
	[Export] public NodePath mMeshInstancePath = "";

	private CollisionShape3D mCollisionShape = null;
	private MeshInstance3D mMeshInstance = null;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mCollisionShape = GetNodeOrNull<CollisionShape3D>(mCollisionShapePath);
		mMeshInstance = GetNodeOrNull<MeshInstance3D>(mMeshInstancePath);

		if(mMeshInstance == null && mCollisionShape == null) { return; }

		GD.PrintErr("[Morphable] Warning: Instance missing a mesh or collider node.");
		QueueFree();
	}

	public override void _Process(double delta)
	{

	}

	public Mesh GetMesh()
	{
		return mMeshInstance.Mesh;
	}
}
