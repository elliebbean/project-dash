using Godot;
using System;

public partial class Main : Node
{
	private int _score;
	private int Score
	{
		get => _score;
		set
		{
			_score = value;
			EmitSignal(SignalName.ScoreChanged, _score);
		}
	}
	[Signal]
	delegate void ScoreChangedEventHandler(int score);
	
	private int _multiplier;
	private int Multiplier
	{
		get => _multiplier;
		set
		{
			_multiplier = value;
			EmitSignal(SignalName.MultiplierChanged, _multiplier);
		}
	}
	[Signal]
	delegate void MultiplierChangedEventHandler(int multiplier);
	
	public void ModifyScore(int value)
	{
		Score += value;
	}
	
	public void ModifyMultiplier(int value)
	{
		Multiplier += value;
	}
	
	public void GameOver()
	{
		GetNode<Timer>("EnemyManager/EnemyTimer").Stop();
	}
	
	public void NewGame()
	{
		Score = 0;
		Multiplier = 0;
		
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);
		GetNode<Timer>("EnemyManager/EnemyTimer").Start();
	}

	public override void _Ready()
	{
	}
}
