using System;
namespace GameEngine
{
	public class Rectangle : HitBox
	{
		public Rectangle(Vector position, Vector size) : base(position)
		{
			m_size = size;
		}

		public override bool Intersects(HitBox other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Point other)
		{
			return other.Position().x <= Position().x + m_size.x / 2 &&
						other.Position().x >= Position().x - m_size.x / 2 &&
				        other.Position().y < Position().y + m_size.y / 2 &&
				        other.Position().y >= Position().y - m_size.y / 2;
		}

		public override bool Intersects(Circle other)
		{
			Vector circleDistance;
			circleDistance.x = Math.Abs(other.Position().x - Position().x);
			circleDistance.y = Math.Abs(other.Position().y - Position().y);

			if (circleDistance.x > (m_size.x / 2 + other.Radius())) { return false; }
			if (circleDistance.y > (m_size.y / 2 + other.Radius())) { return false; }

			if (circleDistance.x <= (m_size.x / 2)) { return true; }
			if (circleDistance.y <= (m_size.y / 2)) { return true; }

			double cornerDistance_sq = Math.Pow(circleDistance.x - m_size.x / 2, 2) +
			                              Math.Pow(circleDistance.y - m_size.y / 2, 2);

			return (cornerDistance_sq <= Math.Pow(other.Radius(), 2));
		}

		public override bool Intersects(Rectangle other)
		{
			bool xIntersect = !(Position().x + m_size.x / 2 < other.Position().x - other.m_size.x / 2 ||
			                    Position().x - m_size.x / 2 > other.Position().x + other.m_size.x / 2);
			bool yIntersect = !(Position().y + m_size.y / 2 < other.Position().y - other.m_size.y / 2 ||
			                    Position().y - m_size.y / 2 > other.Position().y + other.m_size.y / 2);


			return xIntersect && yIntersect;
		}

		public override bool Intersects(Compound other)
		{
			return other.Intersects(this);
		}

		public override EShape GetShape()
		{
			return EShape.Square;
		}

		private Vector m_size;
	}
}
