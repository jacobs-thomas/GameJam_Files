using Godot;
using System;

public partial class PlayerSettings : Resource
{
    [Export] public Key Forward { get; private set; }
    [Export] public Key Backward { get; private set; }
    [Export] public Key Left { get; private set; }
    [Export] public Key Right { get; private set; }
}
