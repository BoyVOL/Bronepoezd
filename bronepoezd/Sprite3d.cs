using Godot;
using System;

public partial class Sprite3d : Sprite3D
{
    [Export]
    Train train = null;

    public override void _Process(double delta)
    {
        base._Process(delta);
        Rotation = new Vector3(0, -train.Rotation, 0);
    }

}
