using Godot;
using System;

[GlobalClass]
public partial class Train : Node2D
{
	[Export]
	Rail CurrentRail = null;

	[Export]
	bool reverse = false;

	[Export]
	float railPos = 0;

	public void SnapToRail(Rail rail, float position)
	{
		this.Transform = rail.Transform * rail.Curve.SampleBakedWithRotation(position);
		if (reverse) Rotation += MathF.PI;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	public void GoToNextRail()
	{
		if (CurrentRail.NextRail != null)
		{
			CurrentRail = CurrentRail.NextRail;
			if (CurrentRail.NextRailReverse)
			{
				railPos = CurrentRail.Curve.GetBakedLength();
				reverse = !reverse;
			}
			else
			{
				railPos = 0;
			}
		}
		else
		{
			CurrentRail = null;
		}
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
				GoToPrevRail();
			}
			if (railPos > CurrentRail.Curve.GetBakedLength())
			{
				GoToNextRail();
			}
		}
    }

	public void GoToPrevRail()
	{
		if (CurrentRail.PrevRail != null)
		{
			CurrentRail = CurrentRail.PrevRail;
			if (CurrentRail.PrevRailReverse)
			{
				railPos = 0;
				reverse = !reverse;
			}
			else
			{
				railPos = CurrentRail.Curve.GetBakedLength();
			}
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
