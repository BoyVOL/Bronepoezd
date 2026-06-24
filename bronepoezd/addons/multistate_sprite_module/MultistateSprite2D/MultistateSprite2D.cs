using Godot;
using System;

[Tool]
[GlobalClass]
public partial class MultistateSprite2D : Sprite2D
{
    [Export]Texture2D[] States = new Texture2D[4];

    [Export]int CurrentState = 0;

    [ExportToolButton("TestState")]
    public Callable TestChange => Callable.From(ToCurrentState);

    public void ChangeState(int StateId)
    {
        Texture = States[StateId];
    }

    public void ToCurrentState()
    {
        ChangeState(CurrentState);
    }
}
