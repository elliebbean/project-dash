using Godot;
using System;

public partial class Player : CharacterBody2D
{
  [Export]
  public int Speed { get; set; } = 400;

  public override void _PhysicsProcess(double delta)
  {
    var mousePosition = GetViewport().GetMousePosition();
    Velocity = (mousePosition - GlobalPosition).Normalized() * Speed;

    MoveAndSlide();
  }
}
