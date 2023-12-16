public record Vector2(int x = 0, int y = 0)
{
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
}