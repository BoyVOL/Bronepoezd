using Godot;
using System;

[GlobalClass]
public partial class Rail : Path2D,IRail
{
    [Export] public Rail NextRail = null;
    [Export] public bool NextRailReverse = false;
    [Export] public Rail PrevRail = null;
    [Export] public bool PrevRailReverse = false;
}
