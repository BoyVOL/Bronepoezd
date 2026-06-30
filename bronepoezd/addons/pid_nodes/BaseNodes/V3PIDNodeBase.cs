using Godot;
using System;
using PID;


[GlobalClass]
public partial class V3PIDNodeBase : Node
{
    [Export] protected RigidBody3D ControlledNode = null;

	[Export]protected Vector3 PID = new Vector3(0.01F,0.01F,0.01F);

    [Export]public bool ExcludeX = false;
    [Export]public bool ExcludeY = false;
    [Export]public bool ExcludeZ = false;

	[Export]float MoveImpulseClamp = 100F;

	protected BaseV3PID PIDControl = new V3PID();

    
	public override void _PhysicsProcess(double delta)
	{
        if(ControlledNode != null){
            PIDControl.P = PID.X;
            PIDControl.I = PID.Y;
            PIDControl.D = PID.Z;
        }
		base._PhysicsProcess(delta);
    }
}
