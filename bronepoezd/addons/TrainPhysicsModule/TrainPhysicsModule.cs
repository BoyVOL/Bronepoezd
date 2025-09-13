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
        AddCustomType("Rail", "Path2D", GD.Load<Script>(GlobalPath + "/Nodes/Rail/Rail.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Rail/icon.png"));
        AddCustomType("Train", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/Train/Train.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Train/icon.png"));
        AddCustomType("RailSwitch", "Rail", GD.Load<Script>(GlobalPath + "/Nodes/RailSwitch/RailSwitch.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/RailSwitch/icon.png"));
		// Initialization of the plugin goes here.
        GD.Print("TrainPhysicsModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("RailSwitch");
		RemoveCustomType("Rail");
		RemoveCustomType("Train");
		GD.Print("TrainPhysicsModule unloaded");
	}
}
#endif
