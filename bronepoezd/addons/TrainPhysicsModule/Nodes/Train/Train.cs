using Godot;
using System;

[GlobalClass]
public partial class Train : Node2D
{
	[Export]
	Rail CurrentRail = null;

	[Export]
	float railPos = 0;

	public void SnapToRail(Rail rail, float position)
	{
		this.Transform = rail.Transform * rail.Curve.SampleBakedWithRotation(position);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		if (CurrentRail != null)
		{
			SnapToRail(CurrentRail, railPos);
		}
	}

	public void GoToNextRail()
	{
		if (CurrentRail.NextRail != null)
		{
			CurrentRail = CurrentRail.NextRail;
			railPos = 0;
		}
		else
		{
			CurrentRail = null;
		}
	}

	public void GoToPrevRail()
	{
		if (CurrentRail.PrevRail != null)
		{
			CurrentRail = CurrentRail.PrevRail;
			railPos = CurrentRail.Curve.GetBakedLength();
		}
		else
		{
			CurrentRail = null;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Keycode == Key.W)
			{
				if (CurrentRail != null)
				{
					railPos += 10;
					if (railPos > CurrentRail.Curve.GetBakedLength())
					{
						GoToNextRail();
					}
				}
			}
			if (eventKey.Keycode == Key.S)
			{ 
				if (CurrentRail != null)
				{
					railPos -= 10;
					if (railPos < 0)
					{
						GoToPrevRail();
					}
				}
			}
		}
	}


}
