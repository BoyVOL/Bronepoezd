using Godot;

public static partial class MathExtra
{
    public static Vector2 GetTangent(Vector2 V1, Vector2 V2, Vector2 V3)
    {
        Vector2 line1 = V1 - V2;
        Vector2 line2 = V3 - V2;
        GD.Print(line1, line2);
        Vector2 result = (line2.Normalized() + line1.Normalized()).Normalized().Orthogonal();
        return result;
    }
}
