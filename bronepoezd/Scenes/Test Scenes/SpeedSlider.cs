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
        train.RailAccel = train.Accelerate/MaxValue*Value;
        
    }

}
