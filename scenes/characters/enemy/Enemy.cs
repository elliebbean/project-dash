using Godot;
using System;

public partial class Enemy : CharacterBase
{
    public override void _Ready()
    {
        base._Ready();
    }

	public override void _PhysicsProcess(double delta)
	{
		
		var target = GetParent().GetParent().GetNode<Player>("Player").GlobalPosition; //This needs to be the player character
		Velocity = (target - GlobalPosition).Normalized() * Speed;

		MoveAndSlide();

		base._PhysicsProcess(delta);

    }
}
