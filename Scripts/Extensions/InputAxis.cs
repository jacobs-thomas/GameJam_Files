using Godot;
using System;

public class InputAxis
{
    // Instance Attributes:
    private string positiveName, negativeName;
    private Key positiveKey, negativeKey;


    // Properties:
    public float Value
    {
        get
        {
            return Input.GetAxis(negativeName, positiveName);
        }
    }


    // Constructor:
    public InputAxis(string name, Key positiveKey, Key negativeKey)
    {
        this.positiveName = $"{name}_positive";
        this.negativeName = $"{name}_negative";
        this.positiveKey = positiveKey;
        this.negativeKey = negativeKey;


        if (!InputMap.HasAction(positiveName))
        {
            // Add the new action
            InputMap.AddAction(positiveName);

            // Create an InputEventKey for the specific key
            InputEventKey keyEvent = new InputEventKey();
            keyEvent.Keycode = positiveKey;

            // Add the key event to the action
            InputMap.ActionAddEvent(positiveName, keyEvent);
        }

        if (!InputMap.HasAction(negativeName))
        {
            // Add the new action
            InputMap.AddAction(negativeName);

            // Create an InputEventKey for the specific key
            InputEventKey keyEvent = new InputEventKey();
            keyEvent.Keycode = negativeKey;

            // Add the key event to the action
            InputMap.ActionAddEvent(negativeName, keyEvent);
        }
    }


    // Class Methods:
    public static void AddAction(in string name, in Key key)
    {
        if (!InputMap.HasAction(name))
        {
            // Add the new action
            InputMap.AddAction(name);

            // Create an InputEventKey for the specific key
            InputEventKey keyEvent = new InputEventKey();
            keyEvent.Keycode = key;

            // Add the key event to the action
            InputMap.ActionAddEvent(name, keyEvent);
        }
    }
}
