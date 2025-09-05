#if TOOLS
using Godot;
using System;

[Tool]
public partial class D2TrainTopDown : EditorPlugin
{
    String GlobalPath;
    public override void _EnterTree()
    {
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
        // Initialization of the plugin goes here.
        AddCustomType("TrainCoreTop", "ParentSwitcherNode2D", GD.Load<Script>(GlobalPath + "/Nodes/TrainCoreTop/TrainCoreTop.cs"),GD.Load<Texture2D>(GlobalPath + "/Nodes/TrainCoreTop/icon.png"));
        AddCustomType("VagonTop", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/VagonTop/VagonTop.cs"),GD.Load<Texture2D>(GlobalPath + "/Nodes/VagonTop/icon.png"));
        AddCustomType("LocomotiveTop", "VagonTop", GD.Load<Script>(GlobalPath + "/Nodes/LocomotiveTop/LocomotiveTop.cs"),GD.Load<Texture2D>(GlobalPath + "/Nodes/LocomotiveTop/icon.png"));
        GD.Print("D2TrainTopDown loaded");
    }

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("TrainCoreTop");
		RemoveCustomType("VagonTop");
		RemoveCustomType("LocomotiveTop");
		GD.Print("D2TrainTopDown unloaded");
	}
}
#endif
