using Godot;
using System;
using System.Diagnostics;

public partial class HandController : Node3D
{
	// Instance Attributes:
	private CollectableItem item;


	// Properties:
	public bool IsHoldingItem
	{
		/**
		* Indicates whether the hand is currently holding an item.
		* @return True if an item is held, false otherwise.
		*/


        get => item != null;
    }

	
	// Methods:
	public void Drop()
	{
		/**
		* Drops the currently held item if there is one.
		* The item is removed from the hand and set to null.
		*/


        if (!IsHoldingItem) { return; }

		item.IsPhysicsEnabled = true;
        item.Reparent(GetTree().Root);
        item = null;
    }

    public override void _Ready()
    {
        base._Ready();

		if(!IsHoldingItem) { return; }
    }

    public void Hold(CollectableItem newItem)
	{
        /**
		* Holds a new item with the hand.
		* If the hand is currently holding an item, it will be dropped first.
		* @param newItem The item to be held by the hand.
		* @throws ArgumentNullException If newItem is null.
		*/


        if (newItem == null) { throw new ArgumentNullException(nameof(newItem), "Cannot hold a null item."); }

        // Drop any currently held item before holding the new one.
        Drop();

        // Set the new item and parent it to the hand.
        item = newItem;

        // Ensure the item's transform matches the hand's transform.
		if(item.GetParent() == null)
		{
			AddChild(item);
		}
		else
		{
            item.Reparent(this, false);
        }

		item.Position = Vector3.Zero;
		item.IsPhysicsEnabled = false;
	}
}
