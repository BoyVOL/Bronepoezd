using Godot;
using System;

[GlobalClass]
public partial class Node2DTracker : Node2D
{
    [Export] public Node2D Source = null;

    [Export]
    public Vector2 ProjectionScale = Vector2.One;

    public override void _Process(double delta)
    {
        base._Process(delta);
        Position = Source.Position*ProjectionScale;
        Scale = Source.Scale*ProjectionScale;
        Rotation = Source.Rotation;
    }
}
