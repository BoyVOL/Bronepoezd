using Godot;
using System;
using PID;


[GlobalClass]
public partial class AnglePid : V3PIDNodeBase
{
    public Quaternion DesiredAngle = Quaternion.FromEuler(Vector3.Zero);
    public AnglePid():base()
    {
        PIDControl = new QPID();
    }
    
	public override void _PhysicsProcess(double delta)
	{
        if(ControlledNode != null){
                Vector3 Impulse = ((QPID)PIDControl).newVector(ControlledNode.Quaternion,DesiredAngle,(float)delta);
                if(ExcludeX) Impulse.X = 0;
                if(ExcludeY) Impulse.Y = 0;
                if (ExcludeZ) Impulse.Z = 0;
                ControlledNode.ApplyTorqueImpulse(Impulse);
        }
		base._PhysicsProcess(delta);
	}
}
