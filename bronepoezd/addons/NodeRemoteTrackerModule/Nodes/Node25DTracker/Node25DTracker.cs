using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[GlobalClass]
public partial class Node25DTracker : Node3D
{
    public enum Planes
    {
        XY,
        XZ,
        ZY,
    }

    [Export] public Node2D Source = null;

    [Export]
    public Vector2 ProjectionScale = Vector2.One;

    [Export]
    public Planes TrackingPlane = Planes.XY;

    public override void _Process(double delta)
    {
        base._Process(delta);
        switch (TrackingPlane)
        {
            case Planes.XY:
                {
                    Position = new Vector3(Source.Position.X*ProjectionScale.X, Source.Position.Y*ProjectionScale.Y, 0);
                    Rotation = new Vector3(0, Source.Rotation, 0);
                    break;
                }
            case Planes.XZ:
                {
                    break;
                }
            case Planes.ZY:
                {
                    break;
                }
            default: throw new ArgumentException("Unexplained plane case");
        }
    }
}
