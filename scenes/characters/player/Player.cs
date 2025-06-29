using System;
using Godot;

public partial class Player : CharacterBody2D
{
	public new Vector2 Position
	{
		get { return base.Position; }
		set
		{
			base.Position = value.Clamp(
					GetViewport().GetVisibleRect().Position,
					GetViewport().GetVisibleRect().End
					);
		}
	}

	[Signal]
	delegate void PlayerDiedEventHandler();

	private int _score;
	public int Score
	{
		get => _score;
		set
		{
			_score = value > 0 ? value : 0;
			EmitSignal(SignalName.ScoreChanged, _score);
		}
	}
	[Signal]
	delegate void ScoreChangedEventHandler(int score);

	private int _multiplier;
	public int Multiplier
	{
		get => _multiplier;
		set
		{
			_multiplier = value > 1 ? value : 1;
			EmitSignal(SignalName.MultiplierChanged, _multiplier);
		}
	}
	[Signal]
	delegate void MultiplierChangedEventHandler(int multiplier);

	private AnimatedSprite2D _animatedSprite;

	public override void _Ready()
	{
		Hide();
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
		var fmodBridge = GD.Load<GDScript>("res://scripts/fmod.gd");
		var fmodBridgeNode = (GodotObject)fmodBridge.New();

		Dash dashNode = null;
		if ((dashNode = GetNodeOrNull<Dash>("Dash")) != null)
		{
			if (!dashNode.IsDashing)
			{
				var mousePosition = GetViewport().GetMousePosition();
				Velocity = (mousePosition - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;
			}
		}

		MoveAndSlide();

		// Temp: Die on collision for testing
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			if (((Node)collision.GetCollider()).IsInGroup("enemies"))
			{
				Die();
				fmodBridgeNode.Call("_on_player_died");
			}
		}
	}

	public void Start(Vector2 position)
	{
		Position = position;
		Score = 0;
		Multiplier = 1;
		Show();
	}

	public void ChangeScore(int delta)
	{
		Score += delta * Multiplier;
	}

	public void ChangeMultiplier(int delta)
	{
		Multiplier += delta;
	}

	public void Die()
	{
		Hide();
		EmitSignal(SignalName.PlayerDied);
	}
}
