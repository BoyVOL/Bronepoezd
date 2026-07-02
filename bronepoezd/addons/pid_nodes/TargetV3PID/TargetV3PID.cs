using Godot;
using System;

[GlobalClass]
public partial class TargetV3PID : MovePid
{
    [Export] public float factor = 0f;

    [Export] public Vector3 Target = Vector3.Zero;

    public Vector3 GetTargetVector()
    {
        return Target - ControlledNode.Position;
    }

}
