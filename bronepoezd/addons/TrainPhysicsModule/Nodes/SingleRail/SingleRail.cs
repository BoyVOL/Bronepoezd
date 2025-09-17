using Godot;
using System;

[GlobalClass]
public partial class SingleRail : Rail, IRail
{
    [Export] public Rail NextRail = null;
    [Export] public bool NextRailReverse = false;
    [Export] public Rail PrevRail = null;
    [Export] public bool PrevRailReverse = false;

    public void SwapToRail(Train train, IRail rail)
    {
        if (train.railPos > Curve.GetBakedLength() / 2)
        {
            if (rail.Curve.GetClosestOffset(Curve.SampleBaked(Curve.GetBakedLength())) > rail.Curve.GetBakedLength() / 2)
            {
                //Head of old rail, head of new rail
                train.railPos = rail.Curve.GetBakedLength();
                train.reverse = !train.reverse;
            }
            else
            {
                //HEad of old rail, tail of new rail                
                train.railPos = 0;
            }
        }
        else
        {
            if (rail.Curve.GetClosestOffset(Curve.SampleBaked(Curve.GetBakedLength())) > rail.Curve.GetBakedLength() / 2)
            {
                //Tail of old rail, head of new rail
                train.railPos = rail.Curve.GetBakedLength();
            }
            else
            {
                //Tail of old rail, tail of new rail
                train.railPos = 0;
                train.reverse = !train.reverse;
            }
        }
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
}
