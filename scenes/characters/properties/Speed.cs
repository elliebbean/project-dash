using Godot;
using System;

public partial class Speed : Node
{
    [Export]
    public int MovementSpeed { get; set; } = 100;
}
