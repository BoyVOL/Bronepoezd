using Godot;
using System;

public partial class Camera2d : Camera2D
{

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
        if (@event is InputEventMouseButton mouse)
        {
            if (mouse.ButtonIndex == MouseButton.WheelUp)
            {
                Zoom /= new Vector2(1.1f, 1.1f);
            }
            if (mouse.ButtonIndex == MouseButton.WheelDown)
            {
                Zoom *= new Vector2(1.1f, 1.1f);
            }
		}
	}
}
