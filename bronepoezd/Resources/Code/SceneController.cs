using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SceneController : Node
{
    [Export] public Node RailSource = null;

    [Export] public Node2D Train = null;

    public void LinkSources()
    {
        List<Node> Kids = TrainPhysicsExtra.GetAllKids(GetChildren(true));
        GD.Print("Test"+Kids.Count);
        foreach (Node Kid in Kids)
        {
            if (Kid is RailVisualiser)
            {
                ((RailVisualiser)Kid).Source = RailSource;
                ((RailVisualiser)Kid).AddTemplateInstances();
            }
            if(Kid is Node2DTracker)
            {
                ((Node2DTracker)Kid).Source = Train;
            }
            if(Kid is Node25DTracker)
            {
                ((Node25DTracker)Kid).Source = Train;
            }
        }
    }
    public override void _EnterTree()
    {
        base._EnterTree();
        LinkSources();
    }

}
