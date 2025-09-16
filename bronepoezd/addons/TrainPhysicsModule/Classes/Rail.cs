using Godot;
using System;

[GlobalClass]
public partial class Rail : Path2D, IRail
{
    [Export] public Rail NextRail = null;
    [Export] public bool NextRailReverse = false;
    [Export] public Rail PrevRail = null;
    [Export] public bool PrevRailReverse = false;

    Curve2D IRail.Curve
    {
        get { return this.Curve; }
    }

    void IRail.MoveToNext(Train train)
    {
        if (NextRail != null)
        {
            train.CurrentRail = NextRail;
            if (NextRailReverse)
            {
                train.railPos = NextRail.Curve.GetBakedLength();
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
        if (PrevRail != null)
        {
            train.CurrentRail = PrevRail;
            if (PrevRailReverse)
            {
                train.railPos = 0;
                train.reverse = !train.reverse;
            }
            else
            {
                train.railPos = PrevRail.Curve.GetBakedLength();
            }
        }
        else
        {
            train.CurrentRail = null;
        }
    }

    Transform2D IRail.Transform {
        get { return this.Transform; }
    }

}
