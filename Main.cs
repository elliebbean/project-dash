using Godot;
using System;

public partial class Main : Node
{
	public void GameOver()
	{
		GetNode<Timer>("EnemyManager/EnemyTimer").Stop();
		GetTree().CallGroup("enemies", "queue_free");
	}
	
	public void NewGame()
	{
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position);
		GetNode<Timer>("EnemyManager/EnemyTimer").Start();
	}

	public override void _Ready()
	{
	}
}
