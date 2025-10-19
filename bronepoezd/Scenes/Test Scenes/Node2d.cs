using Godot;

[Tool]
public partial class Node2d : Node2D
{
	[Export]
	Vector2 Vec1 = Vector2.Zero;

	[Export]
	Vector2 Vec2 = Vector2.Zero;

	[Export]
	Vector2 Vec3 = Vector2.Zero;

	[Export]
	Vector2 Result = Vector2.Zero;
	
	[ExportToolButton("TestButton")]
	public Callable TestButton => Callable.From(Test);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void Test()
	{
		Result = MathExtra.GetTangent(Vec1, Vec2, Vec3);
		QueueRedraw();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Draw()
    {
        base._Draw();
		DrawLine(Vec2, Vec1, Colors.Red);		
		DrawLine(Vec2, Vec3, Colors.Red);			
		DrawLine(Vec2, Vec2+Result, Colors.Red);	
    }

}
