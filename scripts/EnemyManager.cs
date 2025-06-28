using Godot;
using System;

public partial class EnemyManager : Node
{
	public void AddEnemy()
	{
		var enemyScene = ResourceLoader.Load<PackedScene>("res://scenes/Characters/enemy/enemy.tscn");
		var enemy = enemyScene.Instantiate<Enemy>();
		enemy.Position = GetEnemySpawnPosition();
		AddChild(enemy);
	}

	private Vector2 GetEnemySpawnPosition()
	{
		float x;
		float y;
		int offset = 50;
		Rect2 ViewArea = GetViewport().GetVisibleRect();
		switch (Random.Shared.Next(4))
		{
			case 0:
				x = ViewArea.Position.X - offset;
				y = (Random.Shared.NextSingle() * (ViewArea.End.Y + 2 * offset)) - offset;
				break;
			case 1:
				x = (Random.Shared.NextSingle() * (ViewArea.End.X + 2 * offset)) - offset;
				y = ViewArea.Position.Y - offset;
				break;
			case 2:
				x = ViewArea.End.X + offset;
				y = (Random.Shared.NextSingle() * (ViewArea.End.Y + 2 * offset)) - offset;
				break;
			case 3:
				x = (Random.Shared.NextSingle() * (ViewArea.End.X + 2 * offset)) - offset;
				y = ViewArea.End.Y + offset;
				break;
			default:
				x = offset * -1;
				y = offset * -1;
				break;
		}
		return new Vector2(x, y);
	}
}
