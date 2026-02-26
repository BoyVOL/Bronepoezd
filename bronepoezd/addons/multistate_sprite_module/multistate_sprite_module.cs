#if TOOLS
using Godot;
using System;

[Tool]
public partial class multistate_sprite_module : EditorPlugin
{
    String GlobalPath;

	public override void _EnterTree()
	{
        GlobalPath = ((Resource)GetScript()).ResourcePath.GetBaseDir();
		// Initialization of the plugin goes here.
        AddCustomType("MultistateSprite2D", "Sprite2D", GD.Load<Script>(GlobalPath + "/MultistateSprite2D/MultistateSprite2D.cs"),
        GD.Load<Texture2D>(GlobalPath + "/MultistateSprite2D/icon.png"));
		GD.Print("AutoscalingViewportModule loaded");
	}

	public override void _ExitTree()
	{
		// Clean-up of the plugin goes here.
		RemoveCustomType("MultistateSprite2D");
		GD.Print("AutoscalingViewportModule unloaded");
	}
}
#endif
