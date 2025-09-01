using Godot;
using System;
using System.Drawing;

[Tool]
public partial class Line2d : Line2D
{
    [Export]
    Curve2D Curve = null;

    public override void _Draw()
    {
        base._Draw();
        if (Curve != null)
        {
            Points = Curve.GetBakedPoints();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        Curve.SetPointPosition(0, Curve.GetPointPosition(0) + Vector2.Right * 10);
        GD.Print("Test");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        GD.Print("Test2");
    }

}
