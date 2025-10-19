using Godot;
using System;

public partial class Button2 : Button
{
    MultiRail Parent = null;

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent = GetParent<MultiRail>();
    }

    public override void _Pressed()
    {
        Parent.PrevActiveID++;
        if (Parent.PrevActiveID >= Parent.PrevRails.Length) Parent.PrevActiveID = 0;
        base._Pressed();
    }
}
