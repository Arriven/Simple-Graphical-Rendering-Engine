using System;
using System.Collections.Generic;

namespace GameEngine
{
	public class Compound : HitBox
	{
		public Compound(Vector position) : base(position)
		{
			m_childs = new List<HitBox>();
		}

		public override bool Intersects(HitBox other)
		{
			foreach (var child in m_childs)
			{
				if (child.Intersects(other))
				{
					return true;
				}
			}
			return false;
		}

		public override bool Intersects(Point other)
		{
			return Intersects((HitBox)other);
		}

		public override bool Intersects(Circle other)
		{
			return Intersects((HitBox)other);
		}

		public override bool Intersects(Rectangle other)
		{
			return other.Intersects(this);
		}

		public override bool Intersects(Compound other)
		{
			return Intersects((HitBox)other);
		}

		public void AddChild(HitBox child)
		{
			m_childs.Add(child);
		}

		public override EShape GetShape()
		{
			return EShape.None;
		}

		private List<HitBox> m_childs;
	}
}
