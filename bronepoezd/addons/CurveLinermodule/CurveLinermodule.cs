#if TOOLS
using Godot;
using System;

[Tool]
public partial class CurveLinermodule : EditorPlugin
{
    String GlobalPath;
	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        AddCustomType("CurveLiner", "Line2D", GD.Load<Script>(GlobalPath + "/CurveLiner/CurveLiner.cs"),
        GD.Load<Texture2D>(GlobalPath + "/CurveLiner/icon.png"));
		GD.Print("CurveLinermodule Loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("CurveLiner");
		GD.Print("CurveLinermodule unloaded");
	}
}
#endif
