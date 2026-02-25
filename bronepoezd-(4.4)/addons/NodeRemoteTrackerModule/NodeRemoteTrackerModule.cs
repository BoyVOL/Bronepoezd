#if TOOLS
using Godot;
using System;

[Tool]
public partial class NodeRemoteTrackerModule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        AddCustomType("Node2DTracker", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/Node2DTracker/Node2DTracker.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Node2DTracker/icon.png"));
        AddCustomType("Node3DTracker", "Node3D", GD.Load<Script>(GlobalPath + "/Nodes/Node3DTracker/Node3DTracker.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Node3DTracker/icon.png"));
        AddCustomType("Node25DTracker", "Node3D", GD.Load<Script>(GlobalPath + "/Nodes/Node25DTracker/Node25DTracker.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/Node25DTracker/icon.png"));
		GD.Print("NodeRemoteTrackerModule Loaded");
	}

	public override void _ExitTree()
	{
		RemoveCustomType("Node2DTracker");
		RemoveCustomType("Node3DTracker");
		RemoveCustomType("Node25DTracker");
		// Clean-up of the plugin goes here.
		GD.Print("NodeRemoteTrackerModule unloaded");
	}
}
#endif
