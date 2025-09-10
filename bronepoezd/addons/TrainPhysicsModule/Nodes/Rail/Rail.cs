using Godot;
using System;

[GlobalClass]
public partial class Rail : Path2D
{
    [Export] public Rail NextRail = null;
    [Export] public Rail PrevRail = null;
}
