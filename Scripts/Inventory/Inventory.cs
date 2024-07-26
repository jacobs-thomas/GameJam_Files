using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	// Instance Attributes:
	private List<CollectableItem> items = new List<CollectableItem>();


	// Methods:
	public void AddItem(CollectableItem item)
	{
		items.Add(item);
	}

	public void RemoveItem(CollectableItem item)
	{
		items.Remove(item);

	}

	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
