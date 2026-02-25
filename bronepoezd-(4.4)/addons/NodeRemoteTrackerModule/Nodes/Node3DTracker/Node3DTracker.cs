using Godot;
using System;

[GlobalClass]
public partial class Node3DTracker : Node3D
{
    [Export] public Node3D Source = null;

    [Export]
    public Vector3 ProjectionScale = Vector3.One;

    public override void _Process(double delta)
    {
        base._Process(delta);
        Position = Source.Position*ProjectionScale;
        Scale = Source.Scale*ProjectionScale;
        Rotation = Source.Rotation;
    }
}
