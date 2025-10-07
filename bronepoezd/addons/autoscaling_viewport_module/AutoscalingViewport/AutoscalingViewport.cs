using Godot;
using System;

[GlobalClass]
public partial class AutoscalingViewport : SubViewport
{
    [Export]
    Node ScalingSource = null;

    public override void _EnterTree()
    {
        base._EnterTree();
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
        if(ScalingSource != null) Size = (Vector2I)ScalingSource.GetViewport().GetVisibleRect().Size;
    }
}
