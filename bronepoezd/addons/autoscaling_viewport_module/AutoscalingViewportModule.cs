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
        AddCustomType("AutoscalingViewport", "Line2D", GD.Load<Script>(GlobalPath + "/AutoscalingViewport/AutoscalingViewport.cs"),
        GD.Load<Texture2D>(GlobalPath + "/AutoscalingViewport/icon.png"));
		GD.Print("AutoscalingViewportModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("AutoscalingViewport");
		GD.Print("AutoscalingViewportModule unloaded");
	}
}
#endif
