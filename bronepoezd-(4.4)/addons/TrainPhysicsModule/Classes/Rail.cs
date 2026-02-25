using Godot;
using System;

[Tool]
[GlobalClass]
public partial class Rail : Path2D, IRail
{
    [Export] public Curve Height = new Curve();

    public void SwapToRail(Train train, Rail NextRail)
    {
        train.CurrentRail = NextRail;
        if (train.railPos > Curve.GetBakedLength() / 2)
        {
            train.railPos = NextRail.Curve.GetClosestOffset(Curve.SampleBaked(Curve.GetBakedLength(), true));
            if (!HeadToTail(NextRail))
            {
                //Head of old rail, head of new rail
                train.railPos = NextRail.Curve.GetBakedLength();
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
            train.railPos = NextRail.Curve.GetClosestOffset(Curve.SampleBaked(0, true));
            if (TailToHead(NextRail))
            {
                //Tail of old rail, head of new rail
                train.railPos = NextRail.Curve.GetBakedLength();
            }
            else
            {
                //Tail of old rail, tail of new rail
                train.railPos = 0;
                train.reverse = !train.reverse;
            }
        }
    }

    /// <summary>
    /// Выводит истину, если хвост текущей рельсы ближе к голове сравниваемой, нежели к её хвосту
    /// </summary>
    /// <param name="NextRail">сравниваемая рельса</param>
    /// <returns></returns>
    public bool TailToHead(Rail NextRail)
    {
        Vector2[] Points = Curve.GetBakedPoints();
        Vector2 TailGlobalPoint = Points[0] + GlobalPosition;
        Vector2[] RailPoints = NextRail.Curve.GetBakedPoints();
        Vector2 NextRailHeadGlobalPoint = RailPoints[RailPoints.Length - 1] + NextRail.GlobalPosition;
        Vector2 NextRailTailGlobalPoint = RailPoints[0] + NextRail.GlobalPosition;
        return (TailGlobalPoint - NextRailHeadGlobalPoint).LengthSquared() < (TailGlobalPoint - NextRailTailGlobalPoint).LengthSquared();
    }


    /// <summary>
    /// Выводит истину, если голова текущей рельсы ближе к хвосту сравниваемой, нежели к её голове
    /// </summary>
    /// <param name="NextRail">сравниваемая рельса</param>
    /// <returns></returns>
    public bool HeadToTail(Rail NextRail)
    {
        Vector2[] Points = Curve.GetBakedPoints();
        Vector2 HeadGlobalPoint = Points[Points.Length - 1] + GlobalPosition;
        Vector2[] RailPoints = NextRail.Curve.GetBakedPoints();
        Vector2 NextRailHeadGlobalPoint = RailPoints[RailPoints.Length - 1] + NextRail.GlobalPosition;
        Vector2 NextRailTailGlobalPoint = RailPoints[0] + NextRail.GlobalPosition;
        return (HeadGlobalPoint - NextRailTailGlobalPoint).LengthSquared() < (HeadGlobalPoint - NextRailHeadGlobalPoint).LengthSquared();
    }

    Curve2D IRail.Curve
    {
        get { return this.Curve; }
    }

    Transform2D IRail.Transform
    {
        get
        {
            Transform2D Result = this.Transform;
            return Result;
        }
    }

    Vector2 IRail.GlobalShift
    {
        get { return this.GlobalPosition - this.Position; }
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
