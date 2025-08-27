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
        AddCustomType("TrainHeadSide", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/TrainHeadSide/TrainHeadSide.cs"),GD.Load<Texture2D>(GlobalPath + "/Nodes/TrainHeadSide/icon.png"));
        GD.Print("D2TrainSide loaded");
    }

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("TrainHeadSide");
		GD.Print("D2TrainSide unloaded");
	}
}
#endif
