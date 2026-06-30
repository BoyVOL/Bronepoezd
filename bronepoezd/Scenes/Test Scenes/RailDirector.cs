using Godot;
using System;

public partial class RailDirector : Node
{
    [Export] Node PID = null;
    [Export] Path3D Rail = null;

    MovePid pid = null;
    [Export] float factor = 0f;
    [Export] float Speed = 0f;

    public override void _EnterTree()
    {
        base._EnterTree();
        if(PID != null) pid = (MovePid)PID;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if(pid != null && Rail != null)
        {
            Vector3 TargetVector = (Rail.Curve.GetClosestPoint(pid.ControlledNode.Position) - pid.ControlledNode.Position)*factor;
            float SpeedSampleOffset = Rail.Curve.GetClosestOffset(pid.ControlledNode.Position);
            Vector3 SpeedVector = Rail.Curve.SampleBaked(SpeedSampleOffset+0.1f)-Rail.Curve.SampleBaked(SpeedSampleOffset);
            SpeedVector = SpeedVector.Normalized()*Speed;
            pid.DesiredMove = TargetVector+SpeedVector;
        }
    }
}
