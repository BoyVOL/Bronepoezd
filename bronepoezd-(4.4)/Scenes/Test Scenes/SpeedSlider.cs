using Godot;
using System;

public partial class SpeedSlider : VSlider
{

	[Export]
	public String AccelerateAction = "TrainAccelerate";

    [Export]
    public String DecelerateAction = "TrainDecelerate";

    [Export]
    Train train = null;

    [Export]
    CheckButton reversButton = null;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(AccelerateAction))
        {
            Value += 30;
        }
        if (@event.IsActionPressed(DecelerateAction))
        {
            Value -= 30;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if(reversButton != null && reversButton.ButtonPressed)
        {
            train.RailAccel = train.Accelerate / MaxValue * -Value;
        } else
        {
            train.RailAccel = train.Accelerate/MaxValue*Value;
        }
        
    }

}
