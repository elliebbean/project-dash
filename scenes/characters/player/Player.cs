using Godot;
using System;

public partial class Player : CharacterBody2D
{
  public override void _Ready()
  {
	Hide();
  }

  public override void _PhysicsProcess(double delta)
  {
	var mousePosition = GetViewport().GetMousePosition();
	Velocity = (mousePosition - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;

	MoveAndSlide();
  }

  public void Start(Vector2 position)
  {
	Position = position;
	Show();
  }
}
