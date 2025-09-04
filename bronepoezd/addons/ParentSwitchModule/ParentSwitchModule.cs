#if TOOLS
using Godot;
using System;

[Tool]
public partial class ParentSwitchModule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        AddCustomType("ParentSwitcherNode2D", "Node2D", GD.Load<Script>(GlobalPath + "/ParentSwitcherNode2D/ParentSwitcherNode2D.cs"),
        GD.Load<Texture2D>(GlobalPath + "/ParentSwitcherNode2D/icon.png"));
		GD.Print("ParentSwitchModule Loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("ParentSwitcherNode2D");
		GD.Print("ParentSwitchModule unloaded");
	}
}
#endif
