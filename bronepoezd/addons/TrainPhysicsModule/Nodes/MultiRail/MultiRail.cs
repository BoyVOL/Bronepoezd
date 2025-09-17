using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class MultiRail : Rail, IRail
{
    [Export] public Array<Rail> PrevRails = new Array<Rail>();
    [Export] public Array<Rail> NextRails = new Array<Rail>();
    [Export] public int PrevActiveID = 0;
    [Export] public int NextActiveID = 0;

    void IRail.MoveToNext(Train train)
    {
        if (NextRails[NextActiveID] != null)
        {
            SwapToRail(train, NextRails[NextActiveID]);
        }
        else
        {
            train.CurrentRail = null;
        }
    }

    void IRail.MoveToPrev(Train train)
    {
        if (PrevRails[PrevActiveID] != null)
        {
            SwapToRail(train, PrevRails[PrevActiveID]);
        }
        else
        {
            train.CurrentRail = null;
        }
    }
}
