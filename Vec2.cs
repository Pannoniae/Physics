namespace Physics;

public class Vec2 {
    public double x;
    public double y;

    public Vec2() {
        this.x = 0;
        this.y = 0;
    }

    public Vec2(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public Vec2 add(Vec2 other) {
        return new Vec2(x + other.x, y + other.y);
    }

    public static Vec2 operator +(Vec2 one, Vec2 other) {
        return one.add(other);
    }

    public Vec2 sub(Vec2 other) {
        return new Vec2(x - other.x, y - other.y);
    }

    public Vec2 scale(double scale) {
        return new Vec2(x * scale, y * scale);
    }

    public static Vec2 operator -(Vec2 one, Vec2 other) {
        return one.sub(other);
    }

    public static Vec2 operator *(Vec2 one, double other) {
        return one.scale(other);
    }

    public Vec2 rot(double angle) {
        var result = new Vec2 {
            x = x * Math.Cos(angle) - y * Math.Sin(angle),
            y = x * Math.Sin(angle) + y * Math.Cos(angle)
        };
        return result;
    }

    public double mag => Math.Sqrt(x * x + y * y);

    public double magSq => x * x + y * y;

    public Vec2 normalize() {
        var length = mag;
        var vec = new Vec2(x, y);
        if (length == 0.0) return vec;
        vec.x /= length;
        vec.y /= length;
        return vec;
    }

    public Vec2 unit => new Vec2(1, 1);

    public Vec2 normal() {
        return new Vec2(y, -x).normalize();
    }

    public double dot(Vec2 vec) {
        return x * vec.x + y * vec.y;
    }

    public double cross(Vec2 vec) {
        return x * vec.y - y * vec.x;
    }

    public override bool Equals(object? obj) {
        return obj is Vec2 vec && Equals(vec);
    }

    public bool Equals(Vec2 vec) {
        return x == vec.x && y == vec.y;
    }

    public override int GetHashCode() {
        return HashCode.Combine(x, y);
    }

    public override string ToString() {
        return $"Vec2({x}, {y})";
    }
}