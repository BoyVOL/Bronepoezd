using Godot;
using Godot.Collections;
using System;

public partial class ParentSwitcherNode2D : Node2D
{
	[Export] public ParentSwitcherNode2D OldParent = null;

	[Export] Array<Array<NodePath>> Links = new Array<Array<NodePath>>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void RebaseNodes()
	{
		foreach (Array<NodePath> Link in Links)
		{
			for (int i = 1; i < Link.Count; i++)
			{
				Node Test = GetNode<Node>(Link[i]);
				Test.Reparent(GetNode<Node>(Link[0]),false);
				if (Test is ParentSwitcherNode2D)
				{
					((ParentSwitcherNode2D)Test).OldParent = this;
				}
			}
		}
	}

	public override void _EnterTree()
	{
		base._EnterTree();
		OldParent = GetParentOrNull<ParentSwitcherNode2D>();
		RebaseNodes();
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
