#if TOOLS
using Godot;
using System;

[Tool]
public partial class ProxyPath2dVisualiserModule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
		GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
        AddCustomType("RailVisualiser", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/RailVisualiser/RailVisualiser.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/RailVisualiser/icon.png"));
		// Initialization of the plugin goes here.
	}

	public override void _ExitTree()
	{
		RemoveCustomType("RailVisualiser");
		// Clean-up of the plugin goes here.
	}
}
#endif
