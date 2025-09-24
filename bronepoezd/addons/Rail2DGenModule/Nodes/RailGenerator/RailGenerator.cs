using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

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
	float MaxDevition = 100;

	[Export]
	float StepScale = 100;

	[Export]
	PackedScene StraitRail = null;

	[Export]
	PackedScene Junction = null;

	[ExportToolButton("TestGeneration")]
	public Callable GenerateButton => Callable.From(Generate);

	[ExportToolButton("ClearAll")]
	public Callable ClearButton => Callable.From(RemoveAllChildren);

	public void Generate()
	{
		RemoveAllChildren();
		Vector2[] JunctPoints = GenJunctionPoints(MinGenDistance);
		foreach (var item in JunctPoints)
		{
			GenerateJunction(item);
		}
		foreach (var item in GenerateConnDictionary(JunctPoints))
		{
			Rail rail = GenerateRail(JunctPoints, item.Key);
			Complicate(rail);
		}
	}

	public void RemoveAllChildren()
	{
		foreach (Node child in GetChildren())
		{
			RemoveChild(child);
			child.QueueFree();
		} 
	}

	public Vector2[] GenJunctionPoints(double minDistance = 0)
	{
		Vector2[] Result = new Vector2[JunctCount];
		for (int i = 0; i < Result.Length; i++)
		{
			int retries = 0;
			bool FarEnough = false;
			do
			{
				FarEnough = true;
				Result[i] = new Vector2(GD.Randf() * GenBoundaries.X, GD.Randf() * GenBoundaries.Y);
				for (int j = 0; j < i; j++)
				{
					if ((Result[i] - Result[j]).LengthSquared() < minDistance * minDistance)
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

	public Dictionary<Tuple<int, int>, float> GenerateConnDictionary(Vector2[] points)
	{
		Dictionary<Tuple<int, int>, float> Result = new Dictionary<Tuple<int, int>, float>();
		for (int i = 0; i < points.Length; i++)
		{
			for (int j = 0; j < i; j++)
			{
				Result.Add(new Tuple<int, int>(i, j), 0);
			}
		}
		return Result;
	}

	public void Complicate(Rail rail)
	{
		float Current = StepScale+(GD.Randf()*StepScale - StepScale/2);
		while (Current < rail.Curve.GetBakedLength())
		{
			Vector2 Point = rail.Curve.SampleBaked(Current);
			Point += new Vector2(GD.Randf() * MaxDevition - MaxDevition / 2, GD.Randf() * MaxDevition - MaxDevition/ 2);
			rail.Curve.AddPoint(Point, null, null, rail.Curve.PointCount - 1);
			Current += StepScale+(GD.Randf()*StepScale - StepScale/2);
		}
	}

	public Rail GenerateRail(Vector2 Pos1, Vector2 Pos2)
	{
		if (StraitRail != null)
		{
			SingleRail rail = StraitRail.Instantiate() as SingleRail;
			AddChild(rail);
			rail.Position = Vector2.Zero;
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Curve.ClearPoints();
			rail.Curve.AddPoint(Pos1);
			rail.Curve.AddPoint(Pos2);
			rail.QueueRedraw();
			return rail;
		}
		else throw new Exception("no scene for connection rail");
	}

	public Rail GenerateRail(Vector2[] PointArray,Tuple<int, int> connection)
	{
		return GenerateRail(PointArray[connection.Item1], PointArray[connection.Item2]);
	}

	public void GenerateJunction(Vector2 Pos)
	{
		if (Junction != null)
		{
			SingleRail rail = Junction.Instantiate() as SingleRail;
			AddChild(rail);
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Position = Pos;
			rail.QueueRedraw();
		}
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		Parent = GetParent<Node2D>();
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
