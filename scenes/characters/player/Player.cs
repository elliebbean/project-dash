using Godot;
using System;

public partial class Player : CharacterBody2D
{
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
	
  public override void _Ready()
  {
	Hide();
  }

  public override void _PhysicsProcess(double delta)
  {
	var mousePosition = GetViewport().GetMousePosition();
	Velocity = (mousePosition - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;

	MoveAndSlide();
	
	// Temp: Die on collision for testing
	for (int i = 0; i < GetSlideCollisionCount(); i++) {
		var collision = GetSlideCollision(i);
		if (((Node)collision.GetCollider()).IsInGroup("enemies"))
		{
			Die();
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
