using Godot;
using System;

public partial class Dash : Node
{
    [Export]
    public double DashTime { get; set; } = 0.1;
    [Export]
    public int DashDistance { get; set; } = 300;

    public ulong CooldownMs = 500;
    public ulong LastUsed { get; private set; } = 0;

    internal bool IsDashing { get { return DashTarget != Vector2.Zero; } }
    internal Vector2 DashTarget { get; private set; } = Vector2.Zero;

    private Tween DashTween = null;

    private Player parent;

    public override void _Ready()
    {
        parent = GetParent<Player>();
    }

    private void DashTween_Finished()
    {
        DashTarget = Vector2.Zero;
        DashTween = null;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if(LastUsed + CooldownMs > Time.GetTicksMsec()) { return; } //We're on cooldown

            var mousePosition = GetViewport().GetMousePosition();
            DashTarget = parent.GlobalPosition + ((mousePosition - parent.GlobalPosition).Normalized() * DashDistance);

            DashTween = parent.CreateTween();
            DashTween.TweenProperty(parent, "Position", DashTarget, DashTime);
            DashTween.Finished += DashTween_Finished;

            LastUsed = Time.GetTicksMsec();
        }
    }
}
