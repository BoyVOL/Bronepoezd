using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class MultiRail : Rail, IRail
{
    [Export] public Array<Rail> PrevRails = new Array<Rail>();
    [Export] public Array<Rail> NextRails = new Array<Rail>();
    [Export] public Array<bool> PrevReverses = new Array<bool>();
    [Export] public Array<bool> NextReverses = new Array<bool>();
    [Export] public int PrevActiveID = 0;
    [Export] public int NextActiveID = 0;

    void IRail.MoveToNext(Train train)
    {
        if (NextRails[NextActiveID] != null)
        {
            train.CurrentRail = NextRails[NextActiveID];
            if (NextReverses[NextActiveID])
            {
                train.railPos = NextRails[NextActiveID].Curve.GetBakedLength();
                train.reverse = !train.reverse;
            }
            else
            {
                train.railPos = 0;
            }
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
            train.CurrentRail = PrevRails[PrevActiveID];
            if (PrevReverses[PrevActiveID])
            {
                train.railPos = 0;
                train.reverse = !train.reverse;
            }
            else
            {
                train.railPos = PrevRails[PrevActiveID].Curve.GetBakedLength();
            }
        }
        else
        {
            train.CurrentRail = null;
        }
    }
}
