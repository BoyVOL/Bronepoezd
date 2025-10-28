using Godot;
using System;

[GlobalClass]
[Tool]
public partial class CurveLiner : Line2D
{
    [Export]
    public Path2D Source = null;

	[ExportToolButton("UpdatePoints")]
	public Callable UpdateButton => Callable.From(UpdatePoints);

    public override void _EnterTree()
    {
        base._EnterTree();
        if (Source == null) Source = GetParent<Path2D>();
        UpdatePoints();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void UpdatePoints()
    {
        if(Source != null) Points = Source.Curve.GetBakedPoints();
    }
}
