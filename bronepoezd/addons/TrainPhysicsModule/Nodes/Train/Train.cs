using Godot;
using System;

[GlobalClass]
public partial class Train : Node2D
{
	[Export]
	public Node2D StartRail = null;

	public IRail CurrentRail = null;

	[Export]
	public bool reverse = false;

	[Export]
	public float railPos = 0;

    public override void _EnterTree()
    {
		if(StartRail != null) CurrentRail = (IRail)StartRail;
    }

	public void SnapToRail(IRail rail, float position)
	{
		this.Transform = rail.Transform * rail.Curve.SampleBakedWithRotation(position);
		if (reverse) Rotation += MathF.PI;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		if (CurrentRail != null)
		{
			SnapToRail(CurrentRail, railPos);
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
				if (reverse)
				{
					railPos -= 10;
				}
				else
				{
					railPos += 10;
				}
			}
			if (eventKey.Keycode == Key.S)
			{
				if (reverse)
				{
					railPos += 10;
				}
				else
				{
					railPos -= 10;
				}
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
