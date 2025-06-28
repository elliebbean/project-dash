using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		
		var target = GetParent().GetParent().GetNode<Player>("Player").GlobalPosition; //This needs to be the player character
		Velocity = (target - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;

		MoveAndSlide();

		base._PhysicsProcess(delta);

	}
}
