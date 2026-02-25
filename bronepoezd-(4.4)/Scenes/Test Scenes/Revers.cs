using Godot;
using System;

public partial class Revers : CheckButton
{

    [Export]
    public String ReversAction = "TrainRevers";
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(ReversAction))
        {
            ButtonPressed = !ButtonPressed;
        }
    }
}
