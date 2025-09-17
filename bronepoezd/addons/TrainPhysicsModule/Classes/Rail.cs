using Godot;
using System;

[GlobalClass]
public partial class Rail : Path2D, IRail
{

    public void SwapToRail(Train train, Rail rail)
    {
        train.CurrentRail = rail;
        Vector2[] Points = Curve.GetBakedPoints();
        Vector2[] RailPoints = rail.Curve.GetBakedPoints();
        Vector2 RailHeadGlobalPoint = RailPoints[RailPoints.Length - 1] + rail.GlobalPosition;
        Vector2 RailTailGlobalPoint = RailPoints[0] + rail.GlobalPosition;
        if (train.railPos > Curve.GetBakedLength() / 2)
        {
            train.railPos = rail.Curve.GetClosestOffset(Curve.SampleBaked(Curve.GetBakedLength(), true));
            Vector2 HeadGlobalPoint = Points[Points.Length - 1] + GlobalPosition;
            if ((HeadGlobalPoint - RailHeadGlobalPoint).LengthSquared() < (HeadGlobalPoint - RailTailGlobalPoint).LengthSquared())
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
            train.railPos = rail.Curve.GetClosestOffset(Curve.SampleBaked(0, true));
            Vector2 TailGlobalPoint = Points[0] + GlobalPosition;
            if ((TailGlobalPoint - RailHeadGlobalPoint).LengthSquared() < (TailGlobalPoint - RailTailGlobalPoint).LengthSquared())
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

    Curve2D IRail.Curve
    {
        get { return this.Curve; }
    }

    Transform2D IRail.Transform
    {
        get { return this.Transform; }
    }

    void IRail.MoveToNext(Train train)
    {
        throw new NotImplementedException();
    }

    void IRail.MoveToPrev(Train train)
    {
        throw new NotImplementedException();
    }

}
