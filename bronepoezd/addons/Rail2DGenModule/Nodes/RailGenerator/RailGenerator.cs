using Godot;
using System;
using System.Diagnostics;

[Tool]
[GlobalClass]
public partial class RailGenerator : Node2D
{
	public Node2D Parent;

	[Export]
	Vector2 GenBoundaries = new Vector2(0, 0);

	[Export]
	Color DebugBorderColor = Colors.Aqua;

	[Export]
	int JunctCount = 1;

	[Export]
	double MinGenDistance = 100;

	[Export]
	PackedScene StraitRail;

	[Export]
	PackedScene Junction;

	[ExportToolButton("Generate")]
	public Callable GenerateButton => Callable.From(Generate);

	public void Generate()
	{
		RemoveAllChildren();
		Vector2[] JunctPoints = GenJunctionPoints(MinGenDistance);
		foreach (var item in JunctPoints)
		{
			GenerateJunction(item);
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
						break;
					}
				}
			} while (!FarEnough);
		}
		return Result;
	}

	public void GenerateJunction(Vector2 Pos)
	{
		if (Junction != null)
		{
			Node2D Buff = Junction.Instantiate<Node2D>(PackedScene.GenEditState.Instance);
			AddChild(Buff);
			Buff.Position = Pos;
			Buff.QueueRedraw();
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
