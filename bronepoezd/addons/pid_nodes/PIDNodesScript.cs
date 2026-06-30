#if TOOLS
using Godot;
using System;

[Tool]
public partial class PIDNodesScript : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        AddCustomType("AnglePID", "AnglePID", GD.Load<Script>(GlobalPath + "/AnglePID/AnglePID.cs"),
        GD.Load<Texture2D>(GlobalPath + "/AnglePID/icon.png"));
        AddCustomType("MovePID", "MovePID", GD.Load<Script>(GlobalPath + "/MovePID/MovePID.cs"),
        GD.Load<Texture2D>(GlobalPath + "/MovePID/icon.png"));
		GD.Print("AutoscalingViewportModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("MovePID");
		RemoveCustomType("AnglePID");
		GD.Print("AutoscalingViewportModule unloaded");
	}
}
#endif
