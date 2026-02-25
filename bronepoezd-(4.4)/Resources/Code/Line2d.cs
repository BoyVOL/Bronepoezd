using Godot;
using System;
using System.Drawing;

public partial class Line2d : Line2D
{
    [Export]
    Train train = null;

    public override void _Draw()
    {
        base._Draw();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (train != null)
        {
		    ShaderMaterial ShadMat = Material as ShaderMaterial;
		    ShadMat.SetShaderParameter("speed", (train.RailSpeed/10)*-1);
        }
    }

}
