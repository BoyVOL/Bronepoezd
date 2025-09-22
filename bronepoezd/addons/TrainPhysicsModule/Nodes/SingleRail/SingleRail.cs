using Godot;
using System;

[Tool]
[GlobalClass]
public partial class SingleRail : Rail, IRail
{
    [Export] public Rail NextRail = null;
    [Export] public Rail PrevRail = null;

    void IRail.MoveToNext(Train train)
    {
        if (NextRail != null)
        {
            SwapToRail(train, NextRail);
        }
        else
        {
            train.CurrentRail = null;
        }
    }

    void IRail.MoveToPrev(Train train)
    {
        if (PrevRail != null)
        {
            SwapToRail(train, PrevRail);
        }
        else
        {
            train.CurrentRail = null;
        }
    }
}
