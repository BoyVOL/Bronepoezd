using Godot;
using System;
using PID;

[GlobalClass]
public partial class MovePid : V3PIDNodeBase
{
    public Vector3 DesiredMove = Vector3.Zero;

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
        if(ControlledNode != null){
                Vector3 MoveNew = ((V3PID)PIDControl).newVector(ControlledNode.LinearVelocity,DesiredMove,(float)delta);
                if(ExcludeX) MoveNew.X = 0;
                if(ExcludeY) MoveNew.Y = 0;
                if(ExcludeZ) MoveNew.Z = 0;
                ControlledNode.ApplyImpulse(MoveNew);
        }
	}
}
