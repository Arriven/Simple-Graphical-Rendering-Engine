using System;
namespace GameEngine
{
	public class Circle : HitBox
	{
		public Circle(Vector position, float radius) : base(position)
		{
			m_radius = radius;
		}

		public override bool Intersects(HitBox other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Point other)
		{
			return (other.Position() - Position()).LengthSquared() <
												  Math.Pow(m_radius, 2);
		}

		public override bool Intersects(Circle other)
		{
			return (other.Position() - Position()).LengthSquared() <
				                                  Math.Pow(m_radius + other.m_radius, 2);
		}

		public override bool Intersects(Rectangle other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Compound other)
		{
			return other.Intersects(this);
		}

		public float Radius()
		{
			return m_radius;
		}

		public override EShape GetShape()
		{
			return EShape.Circle;
		}
		private readonly float m_radius;
	}
}
