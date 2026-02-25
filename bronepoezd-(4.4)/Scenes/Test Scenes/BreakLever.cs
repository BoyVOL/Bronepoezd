using Godot;
using System;

public partial class BreakLever : VSlider
{

    [Export]
    public String BreakAction = "TrainBreak";

    [Export]
    public String ReleaseAction = "TrainRelease";
    
    [Export]
    Train train = null;
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(BreakAction))
        {
            Value += 50;
        }
        if (@event.IsActionPressed(ReleaseAction))
        {
            Value -= 50;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        train.RailFriction = train.BrakeForce/MaxValue*Value;
        
    }
}
