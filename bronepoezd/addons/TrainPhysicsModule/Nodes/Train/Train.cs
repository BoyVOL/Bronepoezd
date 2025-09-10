using Godot;
using System;

[GlobalClass]
public partial class Train : Node2D
{
	[Export]
	Rail AttachedRail = null;

	[Export]
	float railPos = 0;

	public void SnapToRail(Rail rail, float position)
	{
		this.Transform = rail.Transform * rail.Curve.SampleBakedWithRotation(position);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (AttachedRail != null)
		{
			SnapToRail(AttachedRail, railPos);
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Keycode == Key.W)
			{
				railPos += 10;
				if (railPos > AttachedRail.Curve.GetBakedLength())
				{
					railPos = 0;
				}
			}
		}
	}


}
