#if TOOLS
using Godot;
using System;

[Tool]
public partial class AutoscalingViewportModule : EditorPlugin
{	
    String GlobalPath;

	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
		GD.Print("AutoscalingViewportModule unloaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		GD.Print("AutoscalingViewportModule unloaded");
	}
}
#endif
