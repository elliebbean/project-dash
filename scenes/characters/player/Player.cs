using Godot;
using System;

public partial class Player : CharacterBody2D
{
  private AnimatedSprite2D _animatedSprite;

  public override void _Ready()
  {
    _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
  }

  public override void _Process(double delta)
  {
    var angle = Vector2.Down.AngleTo(Velocity);

    if (angle >= -Math.PI / 6 && angle <= Math.PI / 6)
    {
      _animatedSprite.Play("down");
    }
    else if (angle >= Math.PI / 6 && angle <= Math.PI / 2)
    {
      _animatedSprite.Play("down_left");
    }
    else if (angle >= Math.PI / 2 && angle <= 5 * Math.PI / 6)
    {
      _animatedSprite.Play("up_left");
    }
    else if (angle <= -Math.PI / 6 && angle >= -Math.PI / 2)
    {
      _animatedSprite.Play("down_right");
    }
    else if (angle <= -Math.PI / 2 && angle >= -5 * Math.PI / 6)
    {
      _animatedSprite.Play("up_right");
    }
    else
    {
      _animatedSprite.Play("up");
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    var mousePosition = GetViewport().GetMousePosition();
    Velocity = (mousePosition - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;

    MoveAndSlide();
  }
}
