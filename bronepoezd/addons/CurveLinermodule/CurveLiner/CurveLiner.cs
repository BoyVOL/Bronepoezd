using Godot;
using System;

[GlobalClass]
[Tool]
public partial class CurveLiner : Line2D
{
    Path2D Parent = null;

	[ExportToolButton("UpdatePoints")]
	public Callable UpdateButton => Callable.From(UpdatePoints);

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent = GetParent<Path2D>();
        UpdatePoints();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void UpdatePoints()
    {
        Points = Parent.Curve.GetBakedPoints();
    }
}
