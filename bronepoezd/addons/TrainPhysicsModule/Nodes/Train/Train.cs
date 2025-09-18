using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[GlobalClass]
public partial class Train : Node2D
{
	[Export]
	public Node2D StartRail = null;

	public IRail CurrentRail = null;

	[Export]
	public bool reverse = false;

	[Export]
	public double RailSpeed = 0;
	
	[Export]
	public double RailAccel = 0;

	[Export]
	public double railPos = 0;

    public override void _EnterTree()
    {
		if(StartRail != null) CurrentRail = (IRail)StartRail;
    }

	public void SnapToRail(IRail rail, double position)
	{
		this.Transform = rail.Transform * rail.Curve.SampleBakedWithRotation((float)position);
		if (reverse) Rotation += MathF.PI;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	public int getDirection() {
		if (reverse) return -1; else return 1;
	}

	public void ProcessSpeed(double delta) {
		railPos += RailSpeed * getDirection()*delta;
	}

	public void ProcessAccel(double delta)
	{
		RailSpeed += RailAccel;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (CurrentRail != null)
		{
			ProcessAccel(delta);
			ProcessSpeed(delta);
			SnapToRail(CurrentRail, railPos);
			RailAccel = 0;
		}

		if (CurrentRail != null)
		{
			if (railPos < 0)
			{
				CurrentRail.MoveToPrev(this);
			}
			if (railPos > CurrentRail.Curve.GetBakedLength())
			{
				CurrentRail.MoveToNext(this);
			}
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Keycode == Key.W)
			{
				RailAccel = 10;
			}
			if (eventKey.Keycode == Key.S)
			{
				RailAccel = -10;
			}
		}
	}


}

public interface IRail
{
	public Curve2D Curve
	{
		get;
	}

	public Transform2D Transform
	{
		get;
	}

	public void MoveToPrev(Train train); 

	public void MoveToNext(Train train); 
}
