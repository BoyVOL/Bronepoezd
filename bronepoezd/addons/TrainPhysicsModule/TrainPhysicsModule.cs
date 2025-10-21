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
        AddCustomType("SingleRail", "Path2D", GD.Load<Script>(GlobalPath + "/Nodes/SingleRail/SingleRail.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/SingleRail/icon.png"));
        AddCustomType("Train", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/Train/Train.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Train/icon.png"));
        AddCustomType("MultiRail", "Rail", GD.Load<Script>(GlobalPath + "/Nodes/MultiRail/MultiRail.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/MultiRail/icon.png"));
        AddCustomType("RailVisualiser", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/RailVisualiser/RailVisualiser.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/RailVisualiser/icon.png"));
		// Initialization of the plugin goes here.
        GD.Print("TrainPhysicsModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("RailVisualiser");
		RemoveCustomType("MultiRail");
		RemoveCustomType("SingleRail");
		RemoveCustomType("Train");
		GD.Print("TrainPhysicsModule unloaded");
	}
}
#endif
