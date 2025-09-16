using Godot;
using System;

[GlobalClass]
public partial class Rail : Path2D, IRail
{

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
