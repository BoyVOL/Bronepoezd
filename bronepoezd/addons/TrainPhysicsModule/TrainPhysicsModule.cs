#if TOOLS
using Godot;
using System;

[Tool]
public partial class TrainPhysicsModule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
		GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        GD.Print("TrainPhysicsModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		GD.Print("TrainPhysicsModule unloaded");
	}
}
#endif
