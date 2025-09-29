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
		foreach (MultiRail item in JunctPoints)
		{
			AddChild(item);
			item.QueueRedraw();
		}
		SingleRail[,] ConnMatrix = GenerateConnMatrix(JunctPoints);
		for (int i = 0; i < ConnMatrix.GetLength(0); i++)
		{
			for (int j = 0; j < ConnMatrix.GetLength(1); j++)
			{
				if (ConnMatrix[i, j] != null)
				{
					AddChild(ConnMatrix[i, j]);
					ConnMatrix[i, j].QueueRedraw();
				}
			}
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

	public SingleRail[,] GenerateConnMatrix(MultiRail[] points)
	{
		SingleRail[,] AllConnections = new SingleRail[points.Length, points.Length];
		for (int i = 0; i < points.Length; i++)
		{
			for (int j = i; j < points.Length; j++)
			{
				if (i != j)
				{
					AllConnections[i, j] = GenerateRail(points[i].Position, points[j].Position);
				}
			}
		}
		SingleRail[,] Result = new SingleRail[points.Length, points.Length];
		for (int i = 0; i < points.Length; i++)
		{
			int[] minIndex = [i,i];
			for (int j = i; j < points.Length; j++)
			{
				GD.Print(AllConnections[i, j] + " " +  minIndex[0] + minIndex[1] + " " + AllConnections[minIndex[0], minIndex[1]]);
				if (AllConnections[minIndex[0], minIndex[1]] != null)
				{
					if (AllConnections[i, j].Curve.GetBakedLength() < AllConnections[minIndex[0], minIndex[1]].Curve.GetBakedLength())
					{
						minIndex = [i, j];
					}
				}
				else
				{
					if(AllConnections[i,j] != null) minIndex = [i, j];
				}
				Result[minIndex[0], minIndex[1]] = AllConnections[minIndex[0], minIndex[1]];
			}
		}
		return Result;
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

	public SingleRail GenerateRail(Vector2 Pos1, Vector2 Pos2)
	{
		if (StraitRail != null)
		{
			SingleRail rail = StraitRail.Instantiate() as SingleRail;
			rail.Position = Vector2.Zero;
			rail.Curve = (Curve2D)rail.Curve.Duplicate();
			rail.Curve.ClearPoints();
			rail.Curve.AddPoint(Pos1);
			rail.Curve.AddPoint(Pos2);
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
