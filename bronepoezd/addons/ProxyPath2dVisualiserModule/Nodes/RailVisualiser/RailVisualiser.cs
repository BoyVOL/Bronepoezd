using Godot;
using System;
using System.Collections.Generic;


[GlobalClass]
public partial class RailVisualiser : Node2D
{
    [Export]
    Node Source = null;

    [Export]
    PackedScene VisualTemplate = null;

    public List<Path2D> GetRails()
    {
        List<Path2D> TempResult = new List<Path2D>();
        if (Source != null)
        {
            Godot.Collections.Array<Node> Temp = Source.GetChildren(true);
            foreach (Node tempNode in Temp)
            {
                if(tempNode is Path2D)
                {
                    TempResult.Add((Path2D)tempNode);
                }
            }
        }
        return TempResult;
    }

    public void AddTemplateInstances()
    {
        if (VisualTemplate != null)
        {
            foreach (Path2D tempNode in GetRails())
            {
                CurveLiner instance = VisualTemplate.Instantiate() as CurveLiner;
                instance.Source = tempNode;
                AddChild(instance);
                instance.UpdatePoints();
            }
        }
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        AddTemplateInstances();
    }

}
