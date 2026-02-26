using Godot;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.XPath;

[Tool]
[GlobalClass]
public partial class RailGenerator : Node2D
{
	public Node2D Parent;

	[Export]
	Vector2 GenBoundaries = new Vector2(0, 0);

	[Export]
	Godot.Color DebugBorderColor = Colors.Aqua;

	[Export]
	int JunctCount = 1;

	[Export]
	double MinGenDistance = 100;

	[Export]
	float MinDevition = 10;

	[Export]
	float MaxDevition = 100;

	[Export]
	float StepScale = 100;

	[Export]
	float MinSmoothingModule = 10;

	[Export]
	PackedScene StraitRail = null;

	[Export]
	PackedScene Junction = null;

	[Export]
	Node2D Train = null;

	[ExportToolButton("TestGeneration")]
	public Callable GenerateButton => Callable.From(TestGenerate);

	[ExportToolButton("ClearAll")]
	public Callable ClearButton => Callable.From(RemoveAllChildren);

	public void TestGenerate()
	{
		Generate();
	}

	public MultiRail[] Generate()
	{
		RemoveAllChildren();
		MultiRail[] JunctPoints = GenJunctionPoints(MinGenDistance);
		GenerateConnections(JunctPoints);
		AddAsChildren(JunctPoints);
		AlignAll(JunctPoints);
		return JunctPoints;
	}

	public void RemoveAllChildren()
	{
		foreach (Node child in GetChildren())
		{
			RemoveChild(child);
			child.QueueFree();
		} 
	}

	public MultiRail[] GenJunctionPoints(double minDistance = 0)
	{
		MultiRail[] Result = new MultiRail[JunctCount];
		for (int i = 0; i < Result.Length; i++)
		{
			int retries = 0;
			bool FarEnough = false;
			do
			{
				FarEnough = true;
				Vector2 Pos = new Vector2 (GD.Randf() * GenBoundaries.X, GD.Randf() * GenBoundaries.Y);
				Result[i] = GenerateJunction(Pos);
				for (int j = 0; j < i; j++)
				{
					if ((Result[i].Position - Result[j].Position).LengthSquared() < minDistance * minDistance)
					{
						FarEnough = false;
						retries++;
						break;
					}
				}
				if (retries > 1000) throw new Exception("Too many tries to generate new point");
			} while (!FarEnough);
		}
		return Result;
	}

	public MultiRail FindClosestEmpty(MultiRail[] rails, MultiRail current)
	{
		MultiRail Result = null;
		foreach (MultiRail rail in rails)
		{
			if (rail != current)
			{
				if (rail.NextRails.Length == 0 && rail.PrevRails.Length == 0){
					if (Result != null)
					{
						bool CloserThanBefore = rail.Position.DistanceSquaredTo(current.Position) < Result.Position.DistanceSquaredTo(current.Position);
						if (CloserThanBefore) Result = rail;
					}
					else
					{
						Result = rail;
					}	
				}
			}
		}
		return Result;
	}

	public void Close(MultiRail[] points)
	{
		MultiRail Start = null;
		MultiRail End = null;
		foreach (var rail in points)
		{
			if (rail.PrevRails.Length == 0) Start = rail;
			if (rail.NextRails.Length == 0) End = rail;
		}
		if (Start != null && End != null) GenerateRail(End, Start);
	}

	public void GenerateConnections(MultiRail[] points)
	{
		MultiRail Current = points[(int)GD.RandRange(0, points.Length - 1)];
		while (Current != null)
		{
			MultiRail Next = FindClosestEmpty(points, Current);
			if (Next != null) GenerateRail(Current, Next);
			Current = Next;
		}
		Close(points);
	}

	public void AlignJuction(MultiRail point)
	{
		Vector2 Center = point.Curve.GetPointPosition(0) - point.Curve.GetPointPosition(point.Curve.PointCount-1);
		float EndAngle = 0;
		float StartAngle = 0;
		foreach (Rail NextRail in point.NextRails)
		{
			if(NextRail.Curve.PointCount > 1)
			{
				EndAngle += Center.AngleTo(NextRail.Curve.GetPointPosition(1));
			}
		}
		EndAngle /= point.NextRails.Count();
		foreach (Rail PrevRail in point.PrevRails)
		{
			if(PrevRail.Curve.PointCount > 1)
			{
				StartAngle += Center.AngleTo(PrevRail.Curve.GetPointPosition(PrevRail.Curve.PointCount-2));
			}
		}
		StartAngle /= point.PrevRails.Count();
		float length = point.Curve.GetBakedLength();
		Vector2 NewStart = Center+Vector2.FromAngle(StartAngle)*length/2;
		Vector2 NewEnd = Center+Vector2.FromAngle(EndAngle)*-length/2;
		GD.Print(NewStart,NewEnd);
		point.Curve.SetPointPosition(0, NewStart);
		point.Curve.SetPointPosition(point.Curve.PointCount-1,NewEnd);
	}

	public void AlignAll(MultiRail[] points)
	{
		foreach (MultiRail point in points){
			AlignJuction(point);
		}
	}

	public void ClampPoints(MultiRail Junct)
	{
		
	}

	public void SmoothOut(Rail rail)
	{
        for (int i = 1; i < rail.Curve.PointCount-1; i++)
		{
			Vector2 Original = rail.Curve.GetPointPosition(i);
			Vector2 Prev = rail.Curve.GetPointPosition(i - 1);
			Vector2 Next = rail.Curve.GetPointPosition(i + 1);
			Vector2 Tangent = MathExtra.GetTangent(Original, Prev, Next);
			Vector2 Out = Tangent;
			Vector2 In = Out * -1;
			float OutModule = Original.DistanceTo(Next)/2;
			float InModule = Original.DistanceTo(Prev) / 2;
			float MidModule = (OutModule + InModule) / 2;
			if (MidModule < MinSmoothingModule) MidModule = MinSmoothingModule;
			rail.Curve.SetPointIn(i, In*MidModule);
			rail.Curve.SetPointOut(i, Out*MidModule);
		}
    }

	public void Complicate(Rail rail)
	{
		float Current = StepScale + (GD.Randf() * StepScale - StepScale / 2);
		int TriesCount = 0;
		while (Current < rail.Curve.GetBakedLength())
		{
			TriesCount++;
			Vector2 Point = rail.Curve.SampleBaked(Current);
			Vector2 Origin = Point;
			int stepCount = 0;
			while(Point.DistanceTo(Origin) < MinDevition)
			{
				stepCount++;
				Point += new Vector2(GD.Randf() * MaxDevition - MaxDevition / 2, GD.Randf() * MaxDevition - MaxDevition / 2);
				if (stepCount > 100) throw new Exception("Too Much Tries to generate deviation point");
            }
			rail.Curve.AddPoint(Point, null, null, rail.Curve.PointCount - 1);
			Current += StepScale + (GD.Randf() * StepScale - StepScale / 2);
			if (TriesCount > 10000) throw new Exception("too much tries to complicate rail");
		}
	}

	public SingleRail GenerateRail(MultiRail Start, MultiRail End)
	{
		if (StraitRail != null)
		{
			SingleRail rail = StraitRail.Instantiate() as SingleRail;
			rail.Position = Vector2.Zero;
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Curve.ClearPoints();
			rail.Curve.AddPoint(Start.GlobalPosition+Start.Curve.GetPointPosition(Start.Curve.PointCount-1)-rail.GlobalPosition);
			rail.Curve.AddPoint(End.GlobalPosition+Start.Curve.GetPointPosition(0)-rail.GlobalPosition);
			rail.PrevRail = Start;
			rail.NextRail = End;
			List<Rail> StartList = Start.NextRails.ToList();
			List<Rail> EndList = End.PrevRails.ToList();
			StartList.Add(rail);
			EndList.Add(rail);
			Start.NextRails = StartList.ToArray();
			End.PrevRails = EndList.ToArray();
			Complicate(rail);
			SmoothOut(rail);
			return rail;
		}
		else throw new Exception("no scene for connection rail");
	}

	public void PlaceTrain(Rail rail)
	{
		if (Train != null)
		{
			Train train = (Train)Train;
			train.CurrentRail = rail;
			float random = GD.Randf();
			if (random > 0.5f)
			{
				train.reverse = false;
			}
			else
			{
				train.reverse = true;
			}
		}
		else throw new Exception("no defined train");
	}

	public MultiRail GenerateJunction(Vector2 Pos)
	{
		MultiRail rail;
		if (Junction != null)
		{
			rail = Junction.Instantiate() as MultiRail;
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Position = Pos; 
		}
		else rail = null;
		return rail;
	}

	public void AddAsChildren(MultiRail[] Junctions) {
		foreach (MultiRail junct in Junctions)
		{
			this.AddChild(junct);
			junct.QueueRedraw();
			foreach (var rail in junct.PrevRails)
			{
				if (rail.GetParent<Node>() != this)
				{
					AddChild(rail);
					rail.QueueRedraw();
				}
			}
			foreach (var rail in junct.NextRails)
			{
				if (rail.GetParent<Node>() != this)
				{
					AddChild(rail);
					rail.QueueRedraw();
				}
			}
		}
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		Parent = GetParent<Node2D>();
		MultiRail[] juncts = Generate();
		PlaceTrain(juncts[0]);
	}


	public override void _Process(double delta)
	{
		base._Process(delta);
		QueueRedraw();
	}

	public override void _Draw()
	{
		base._Draw();
		#if DEBUG
		{
			DrawRect(new Rect2(Vector2.Zero, GenBoundaries), DebugBorderColor, false, 5);
		}
		#endif
	}

}
