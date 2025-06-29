using Godot;
using System;

public partial class PauseScreen : Control
{
    public override void _Process(double delta)
	{
        if (Input.IsActionJustPressed("pause"))
        {
            Show();
		}
	}
    public void OnContinueButtonPressed()
    {
        Hide();
        GetTree().Paused = false;
    }
}
