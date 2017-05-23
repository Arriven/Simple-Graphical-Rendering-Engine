using System;
namespace GameEngine
{
	public struct Vector
	{
		public Vector(float x = 0, float y = 0)
		{
			this.x = x;
			this.y = y;
		}
		public float x;
		public float y;

		public static Vector operator +(Vector lhs, Vector rhs)
		{
			return new Vector(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		public static Vector operator -(Vector lhs, Vector rhs)
		{
			return new Vector(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		public static Vector operator *(Vector lhs, float rhs)
		{
			return new Vector(lhs.x * rhs, lhs.y * rhs);
		}

		public static Vector operator /(Vector lhs, float rhs)
		{
			return new Vector(lhs.x / rhs, lhs.y / rhs);
		}

		public static bool operator ==(Vector lhs, Vector rhs)
		{
			return (lhs.x == rhs.x) && (lhs.y == rhs.y);
		}

		public static bool operator !=(Vector lhs, Vector rhs)
		{
			return (lhs.x != rhs.x) || (lhs.y != rhs.y);
		}

		public float Length()
		{
			return (float)Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
		}

		public float LengthSquared()
		{
			return (float)(Math.Pow(x, 2) + Math.Pow(y, 2));
		}

        public Vector Normalized()
        {
            return this / Length();
        }
	}
}
