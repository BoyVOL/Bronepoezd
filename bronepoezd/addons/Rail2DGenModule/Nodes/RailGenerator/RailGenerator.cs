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
		MultiRail[] JunctPoints = GenJunctionPoints(MinGenDistance);
		GenerateConnections(JunctPoints);
		AddAsChildren(JunctPoints);
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
				Result[i] = GenerateJunction(new Vector2(GD.Randf() * GenBoundaries.X, GD.Randf() * GenBoundaries.Y));
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

	public void GenerateConnections(MultiRail[] points)
	{
		for (int i = 0; i < points.Length; i++)
		{
			for (int j = 0; j < points.Length; j++)
			{
				if (i != j)
				{
					GenerateRail(points[i], points[j]);
				}
			}
		}
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

	public SingleRail GenerateRail(MultiRail Start, MultiRail End)
	{
		if (StraitRail != null)
		{
			SingleRail rail = StraitRail.Instantiate() as SingleRail;
			rail.Position = Vector2.Zero;
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Curve.ClearPoints();
			rail.Curve.AddPoint(Start.Position);
			rail.Curve.AddPoint(End.Position);
			rail.PrevRail = Start;
			rail.NextRail = End;
			List<Rail> StartList = Start.NextRails.ToList();
			List<Rail> EndList = End.PrevRails.ToList();
			StartList.Add(rail);
			EndList.Add(rail);
			Start.NextRails = StartList.ToArray();
			End.PrevRails = EndList.ToArray();
			Complicate(rail);
			return rail;
		}
		else throw new Exception("no scene for connection rail");
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
		Generate();
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
