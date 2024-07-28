using Godot;
using System;

public partial class DialogManager : Node
{

	// Class Attributes:
	private static DialogManager singletonInstance = null;
	private Label label = null;
	

	// Class Properties:
	public static DialogManager Singleton
	{
		get
		{
			if(singletonInstance == null)
			{
				singletonInstance = new DialogManager();
				singletonInstance.GetTree().Root.AddChild(singletonInstance);
				singletonInstance.label = new Label();
				singletonInstance.AddChild(singletonInstance.label);
			}

			return singletonInstance;
		}
	}


	// Methods:
	public void SetText(string text)
	{
		label.Text = text;
	}
}
