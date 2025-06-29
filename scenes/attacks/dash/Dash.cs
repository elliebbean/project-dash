using Godot;
using System;
using System.Diagnostics;

public partial class Dash : Node
{
    [Export]
    public double DashTime { get; set; } = 0.1;
    [Export]
    public int DashSpeed { get; set; } = 1000;

    public ulong CooldownMs = 500;
    public ulong LastUsed { get; private set; } = 0;

    internal bool IsDashing { get; private set; }
    internal Vector2 DashTarget { get; private set; } = Vector2.Zero;

    private Tween DashTween = null;

    private Player parent;
    private Timer dashTimer;

    public override void _Ready()
    {
        parent = GetParent<Player>();
        dashTimer = GetNode<Timer>("DashTimer");
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
            if (IsDashing) { return; }
            if (LastUsed + CooldownMs > Time.GetTicksMsec()) { return; } //We're on cooldown

            dashTimer.Start();
            var mousePosition = GetViewport().GetMousePosition();
            Debug.WriteLine(parent.Velocity);
            parent.Velocity = (mousePosition - parent.GlobalPosition).Normalized() * DashSpeed;
            Debug.WriteLine(parent.Velocity);
            IsDashing = true;

            LastUsed = Time.GetTicksMsec();
        }
    }

    public void OnDashTimerTimeout()
    {
        parent.Velocity = Vector2.Zero;
        IsDashing = false;
    }
}
