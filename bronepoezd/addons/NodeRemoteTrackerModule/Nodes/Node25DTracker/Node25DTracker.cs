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
        YZ,
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
                    Rotation = new Vector3(0, 0, Source.Rotation);
                    break;
                }
            case Planes.XZ:
                {
                    Position = new Vector3(Source.Position.X*ProjectionScale.X, 0, Source.Position.Y*ProjectionScale.Y);
                    Rotation = new Vector3(0, 0, Source.Rotation);
                    break;
                }
            case Planes.YZ:
                {
                    Position = new Vector3(0, Source.Position.X*ProjectionScale.X, Source.Position.Y*ProjectionScale.Y);
                    Rotation = new Vector3(Source.Rotation, 0, 0);
                    break;
                }
            default: throw new ArgumentException("Unexplained plane case");
        }
    }
}
