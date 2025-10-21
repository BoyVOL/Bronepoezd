using Godot;
using System;

[Tool]
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
        if(ScalingSource != null)
        {
            if(ScalingSource is Control)
            {
                Control Container = (Control)ScalingSource;
                Size = (Vector2I)(Container.Size*Scale);
            }
        }
    }
}
