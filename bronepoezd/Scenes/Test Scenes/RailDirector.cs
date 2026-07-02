using Godot;
using System;

public partial class RailDirector : TargetV3PID
{
    [Export] Path3D Rail = null;

    public override void _EnterTree()
    {
        base._EnterTree();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        if(Rail != null)
        {
            Target = Rail.Curve.GetClosestPoint(ControlledNode.Position);
            DesiredMove = GetTargetVector()*factor;
        }
        base._PhysicsProcess(delta);
    }
}
