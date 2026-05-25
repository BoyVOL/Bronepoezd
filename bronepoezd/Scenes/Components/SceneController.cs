using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SceneController : Node
{
    [Export] public Node RailSource = null;

    [Export] Godot.Collections.Array<RailVisualiser> Visualisers = new Godot.Collections.Array<RailVisualiser>();

    [Export] public Node2D Train = null;

    [Export] Godot.Collections.Array<Node2DTracker> Trackers2D = new Godot.Collections.Array<Node2DTracker>();
    [Export] Godot.Collections.Array<Node25DTracker> Trackers25D = new Godot.Collections.Array<Node25DTracker>();

    public void LinkSources()
    {
        GD.Print("Test"+GetAllKids(GetChildren(true)).Count);
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

    /// <summary>
    /// Рекурсивно возвращает всех детей в принципе в виде списка
    /// </summary>
    /// <param name="nodes"></param>
    /// <returns></returns>
    public List<Node> GetAllKids(Godot.Collections.Array<Node> nodes)
    {
        List<Node> Result = new List<Node>();
        foreach (Node item in nodes.ToList<Node>())
        {
            Result.Add(item);
            if (item.GetChildCount() > 0)
            {
                Result.AddRange(GetAllKids(item.GetChildren(true)).ToList<Node>());
            }
        }
        return Result;
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        LinkSources();
    }

}
