using Godot;

public static partial class MathExtra
{
    public static Vector2 GetTangent(Vector2 original, Vector2 Vec1, Vector2 Vec2)
    {
        Vector2 line1 = (Vec1 - original).Normalized();
        Vector2 line2 = (Vec2 - original).Normalized();
        Vector2 result = (line2 + line1).Normalized().Orthogonal();
        if (result.DistanceSquaredTo(line2) > result.DistanceSquaredTo(line1)) result *= -1;
        return result;
    }
}
