using Godot;
using System;

[GlobalClass]
public partial class Node2DTracker : Node2D, Node2DTracking
{
    [Export] public Node2D Source = null;
    public Node2D TrackSource
    {
        get { return Source;}
        set { Source = value;}
    }

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
