using Godot;
using System;

public partial class Hud : Control
{
    public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}
	
    public void UpdateMultiplier(int multiplier)
	{
		GetNode<Label>("MultiplierLabel").Text = multiplier.ToString() + "x";
	}
}
