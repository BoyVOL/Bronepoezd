using Godot;
using System;

[GlobalClass]
public partial class MultiRail : Rail, IRail
{
    Curve2D IRail.Curve
    {
        get { return this.Curve; }
    }
}
