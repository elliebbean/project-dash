using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();
	
	public void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		GetNode<Label>("Title").Hide();
		GetNode<Label>("GameOverLabel").Hide();
		GetNode<Button>("RetryButton").Hide();
		GetNode<Label>("ScoreLabel").Show();
		GetNode<Label>("MultiplierLabel").Show();
		EmitSignal(SignalName.StartGame);
	}
	
	public void GameOver()
	{
		GetNode<Label>("GameOverLabel").Show();
		GetNode<Button>("RetryButton").Show();
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
	public void UpdateMultiplier(int multiplier)
	{
		GetNode<Label>("MultiplierLabel").Text = multiplier.ToString() + "x";
	}
}
