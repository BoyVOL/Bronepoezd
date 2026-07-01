using Godot;
using System;

public partial class RailDirector : MovePid
{
    [Export] Path3D Rail = null;
    [Export] float factor = 0f;

    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _PhysicsProcess(double delta)
    {
        if(Rail != null)
        {
            Vector3 TargetVector = (Rail.Curve.GetClosestPoint(ControlledNode.Position) - ControlledNode.Position)*factor;
            float SpeedSampleOffset = Rail.Curve.GetClosestOffset(ControlledNode.Position);
            DesiredMove = TargetVector;
        }
        base._PhysicsProcess(delta);
    }
}
