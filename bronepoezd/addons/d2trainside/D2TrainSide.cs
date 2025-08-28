#if TOOLS
using Godot;
using System;

[Tool]
public partial class D2TrainSide : EditorPlugin
{
    String GlobalPath;
    public override void _EnterTree()
    {
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
        // Initialization of the plugin goes here.
        AddCustomType("TrainCoreSide", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/TrainCoreSide/TrainCoreSide.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/TrainCoreSide/icon.png"));
        AddCustomType("LocomotiveSide", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/LocomotiveSide/LocomotiveSide.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/LocomotiveSide/icon.png"));
        AddCustomType("VagonSide", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/VagonSide/VagonSide.cs"),
        GD.Load<Texture2D>(GlobalPath + "/Nodes/VagonSide/icon.png"));
        GD.Print("D2TrainSide loaded");
    }

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("TrainCoreSide");
		RemoveCustomType("LocomotiveSide");
		RemoveCustomType("VagonSide");
		GD.Print("D2TrainSide unloaded");
	}
}
#endif
