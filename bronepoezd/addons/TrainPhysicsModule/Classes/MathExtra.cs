using Godot;

public static partial class MathExtra
{
    /// <summary>
    /// Выдаёт вектор, ортогональный к медиане треугольника, сформированного тремя точками, проведённой из вершины Original.
    /// Всегда смотрит в сторону V2
    /// </summary>
    /// <param name="original"></param>
    /// <param name="Vec1"></param>
    /// <param name="Vec2"></param>
    /// <returns></returns>
    public static Vector2 GetTangent(Vector2 original, Vector2 Vec1, Vector2 Vec2)
    {
        Vector2 line1 = (Vec1 - original).Normalized();
        Vector2 line2 = (Vec2 - original).Normalized();
        Vector2 result = (line2 + line1).Normalized().Orthogonal();
        if (result.DistanceSquaredTo(line2) > result.DistanceSquaredTo(line1)) result *= -1;
        return result;
    }
}
