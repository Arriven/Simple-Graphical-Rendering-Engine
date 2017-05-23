using System;
namespace GameEngine
{
	public class Point : HitBox
	{
		public Point(Vector position) : base(position)
		{
		}

		public override bool Intersects(HitBox other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Point other)
		{
			return other.Position() == Position();
		}

		public override bool Intersects(Circle other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Rectangle other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Compound other)
		{
			return other.Intersects(this);
		}

		public override EShape GetShape()
		{
			return EShape.Circle;
		}
	}
}
