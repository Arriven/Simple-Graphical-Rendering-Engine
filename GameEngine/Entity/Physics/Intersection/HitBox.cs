using System;
namespace GameEngine
{
	public abstract class HitBox
	{
		protected HitBox(Vector position)
		{
			m_position = position;
		}
		public abstract bool Intersects(HitBox other);
		public abstract bool Intersects(Point other);
		public abstract bool Intersects(Circle other);
		public abstract bool Intersects(Rectangle other);
		public abstract bool Intersects(Compound other);

		public Vector Position()
		{
			return m_position;
		}

		public void SetPosition(Vector position)
		{
			m_position = position;
		}

		public abstract EShape GetShape();

		private Vector m_position;
	}
}
