#if TOOLS
using Godot;
using System;

[Tool]
public partial class Rail2DGenModule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
		GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
        AddCustomType("RailGenerator", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/RailGenerator/RailGenerator.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/RailGenerator/icon.png"));
		// Initialization of the plugin goes here.
        GD.Print("Rail2DGenModule loaded");
	}

	public override void _ExitTree()
	{
		RemoveCustomType("RailGenerator");
		// Clean-up of the plugin goes here.
		GD.Print("Rail2DGenModule unloaded");
	}
}
#endif
