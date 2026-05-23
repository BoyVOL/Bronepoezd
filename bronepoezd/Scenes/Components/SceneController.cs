using Godot;
using System;

public partial class SceneController : Node
{
    [Export] public Node RailSource = null;

    [Export] Godot.Collections.Array<RailVisualiser> Visualisers = new Godot.Collections.Array<RailVisualiser>();

    [Export] public Node2D Train = null;

    [Export] Godot.Collections.Array<Node2DTracker> Trackers2D = new Godot.Collections.Array<Node2DTracker>();
    [Export] Godot.Collections.Array<Node25DTracker> Trackers25D = new Godot.Collections.Array<Node25DTracker>();

    public void LinkSources()
    {
        if(RailSource != null)
        {
            foreach (RailVisualiser kid in Visualisers)
            {
                    kid.Source = RailSource;
                    kid.AddTemplateInstances();
            }
        }
        if(Train != null)
        {
            foreach (Node2DTracker kid in Trackers2D)
            {
                    kid.Source = Train;
            }
            foreach (Node25DTracker kid in Trackers25D)
            {
                    kid.Source = Train;
            }
        }
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        LinkSources();
    }

}
