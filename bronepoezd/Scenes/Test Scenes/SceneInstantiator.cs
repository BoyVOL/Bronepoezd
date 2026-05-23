using Godot;
using System;

public partial class SceneInstantiator : Node
{
    [Export]
    public PackedScene InstantiatedScene = null;
    [Export] Node RailSource = null;
    [Export] Node2D Train = null;

    public SceneController SpawnScene()
    {
        SceneController Result = null;
        if(InstantiatedScene != null)
        {
            Result = InstantiatedScene.Instantiate() as SceneController;
        }
        return Result;
    }

    public void ConstructScene()
    {
        SceneController Scene = SpawnScene();
        if(Scene != null && RailSource != null && Train != null)
        {
            AddChild(Scene);
            Scene.RailSource = RailSource;
            Scene.Train = Train;
            Scene.LinkSources();
        }
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        ConstructScene();
    }
}
