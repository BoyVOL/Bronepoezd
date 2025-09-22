using Godot;
using System;
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
	PackedScene StraitRail = null;

	[Export]
	PackedScene Junction = null;

	[ExportToolButton("Generate")]
	public Callable GenerateButton => Callable.From(Generate);

	public void Generate()
	{
		RemoveAllChildren();
		Vector2[] JunctPoints = GenJunctionPoints(MinGenDistance);
		foreach (var item in JunctPoints)
		{
			GenerateJunction(item);
			foreach (var item2 in JunctPoints)
			{
				GenerateRail(item, item2);
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
				if (retries > 1000) throw new Exception("Too many tries to generate rail network");
			} while (!FarEnough);
		}
		return Result;
	}

	public void GenerateRail(Vector2 Pos1, Vector2 Pos2)
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
		}
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
