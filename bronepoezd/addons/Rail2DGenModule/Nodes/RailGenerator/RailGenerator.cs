using Godot;
using System;

[Tool]
[GlobalClass]
public partial class RailGenerator : Node
{
    [ExportToolButton("Generate")]
    public Callable GenerateButton => Callable.From(Generate);

    public void Generate()
    {
        
    }
}
