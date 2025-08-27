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
        AddCustomType("TrainHead", "Node2D", GD.Load<Script>(GlobalPath + "/Nodes/TrainHead/TrainHead.cs"),GD.Load<Texture2D>(GlobalPath + "/Nodes/TrainHead/icon.png"));
        GD.Print("D2TrainTopDown loaded");
    }

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("TrainHead");
		GD.Print("D2TrainTopDown unloaded");
	}
}
#endif
