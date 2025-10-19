using Godot;
using System;

public partial class Button : Godot.Button
{
    MultiRail Parent = null;

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent = GetParent<MultiRail>();
    }

    public override void _Pressed()
    {
        base._Pressed();
        Parent.NextActiveID++;
        if (Parent.NextActiveID >= Parent.NextRails.Length) Parent.NextActiveID = 0;
        Text = Parent.NextActiveID.ToString();
    }
}
