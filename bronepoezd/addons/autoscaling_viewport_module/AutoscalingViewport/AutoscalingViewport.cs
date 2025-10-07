using Godot;
using System;

[GlobalClass]
public partial class AutoscalingViewport : SubViewport
{
    [Export]
    Node ScalingSource = null;

    [Export]
    Vector2 Scale = Vector2.One;

    public override void _EnterTree()
    {
        base._EnterTree();
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
        if(ScalingSource != null) Size = (Vector2I)(ScalingSource.GetViewport().GetVisibleRect().Size*Scale);
    }
}
