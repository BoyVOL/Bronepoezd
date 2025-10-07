using Godot;
using System;

public partial class SubViewport : Godot.SubViewport
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
        Size = (Vector2I)ScalingSource.GetViewport().GetVisibleRect().Size;
    }
}
