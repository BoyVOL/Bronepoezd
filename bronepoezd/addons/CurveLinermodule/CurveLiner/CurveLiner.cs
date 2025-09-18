using Godot;
using System;

[GlobalClass]
[Tool]
public partial class CurveLiner : Line2D
{
    Path2D Parent = null;

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent = GetParent<Path2D>();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Points = Parent.Curve.GetBakedPoints();
    }


}
