using Godot;
using System;

public partial class CharacterBase : CharacterBody2D
{
    [Export]
    public virtual int Speed { get; set; } = 100;
}
