using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

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
		AlgoJunct[] JunctPoints = GenJunctionPoints(MinGenDistance);
		foreach (var item in JunctPoints)
		{
			GenerateJunction(item.Pos);
		}
		Dictionary<linkIndexes, float> ConnDict = GenerateConnDictionary(JunctPoints);
		TravelerGreedy(JunctPoints, ConnDict);
		foreach (var item in ConnDict)
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

	public struct AlgoJunct
	{
		public Vector2 Pos;

		public int fluff = 0;

		public AlgoJunct(Vector2 pos)
		{
			Pos = pos;
		}

		public AlgoJunct()
		{
			Pos = Vector2.Zero;
		}
	}

	public AlgoJunct[] GenJunctionPoints(double minDistance = 0)
	{
		AlgoJunct[] Result = new AlgoJunct[JunctCount];
		for (int i = 0; i < Result.Length; i++)
		{
			int retries = 0;
			bool FarEnough = false;
			do
			{
				FarEnough = true;
				Result[i] = new AlgoJunct(new Vector2(GD.Randf() * GenBoundaries.X, GD.Randf() * GenBoundaries.Y));
				for (int j = 0; j < i; j++)
				{
					if ((Result[i].Pos - Result[j].Pos).LengthSquared() < minDistance * minDistance)
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

	public struct linkIndexes {
		public int StartId;
		public int EndId;

		public linkIndexes(int startId, int endId)
		{
			StartId = startId;
			EndId = endId;
		}
	}

	public Dictionary<linkIndexes, float> GenerateConnDictionary(Array points)
	{
		Dictionary<linkIndexes, float> Result = new Dictionary<linkIndexes, float>();
		for (int i = 0; i < points.Length; i++)
		{
			for (int j = 0; j < i; j++)
			{
				Result.Add(new linkIndexes(i, j), 0);
			}
		}
		return Result;
	}

	public void CalcWeights(AlgoJunct[] junctions, Dictionary<linkIndexes, float> connections)
	{
		foreach (var item in connections)
		{
			float weight = (junctions[item.Key.EndId].Pos - junctions[item.Key.StartId].Pos).LengthSquared();
			connections[item.Key] = weight;
		}
	}

	public void TravelerGreedy(AlgoJunct[] junctions, Dictionary<linkIndexes, float> connections)
	{
		int CurrentId = (int)GD.RandRange(0, junctions.Length);
	}

	public void Complicate(Rail rail)
	{
		float Current = StepScale + (GD.Randf() * StepScale - StepScale / 2);
		while (Current < rail.Curve.GetBakedLength())
		{
			Vector2 Point = rail.Curve.SampleBaked(Current);
			Point += new Vector2(GD.Randf() * MaxDevition - MaxDevition / 2, GD.Randf() * MaxDevition - MaxDevition / 2);
			rail.Curve.AddPoint(Point, null, null, rail.Curve.PointCount - 1);
			Current += StepScale + (GD.Randf() * StepScale - StepScale / 2);
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

	public Rail GenerateRail(AlgoJunct[] PointArray,linkIndexes connection)
	{
		return GenerateRail(PointArray[connection.StartId].Pos, PointArray[connection.EndId].Pos);
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
