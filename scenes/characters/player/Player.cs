using Godot;

public partial class Player : CharacterBody2D
{
    public new Vector2 Position
    {
        get { return base.Position; }
        set
        {
            base.Position = value.Clamp(
                GetViewport().GetVisibleRect().Position,
                GetViewport().GetVisibleRect().End
                );
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Dash dashNode = null;
        if ((dashNode = GetNodeOrNull<Dash>("Dash")) != null)
        {
            if (dashNode.IsDashing)
            {
                return;
            }
        }

        var mousePosition = GetViewport().GetMousePosition();
        Velocity = (mousePosition - GlobalPosition).Normalized() * GetNode<Speed>("Speed").MovementSpeed;

        MoveAndSlide();
    }
}
