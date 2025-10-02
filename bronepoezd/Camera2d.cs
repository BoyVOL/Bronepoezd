using Godot;
using System;

public partial class Camera2d : Camera2D
{
    [Export]
    public String ZoomInAction = "ZoomIn";
    
    [Export]
    public String ZoomOutAction = "ZoomOut";

	public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
		if (@event.IsAction(ZoomInAction)){
                Zoom /= new Vector2(1.1f, 1.1f);
		}
		if (@event.IsAction(ZoomOutAction)){
                Zoom *= new Vector2(1.1f, 1.1f);
		}
    }
}
