using Godot;
using System;

public partial class UI : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
	public void OnStartButtonPressed()
	{
		GetNode<Control>("TitleScreen").Hide();
		GetNode<Control>("GameOverScreen").Hide();
		GetNode<Control>("HUD").Show();
		EmitSignal(SignalName.StartGame);
	}
	
	public void GameOver()
	{
		GetNode<Control>("GameOverScreen").Show();
	}
}
