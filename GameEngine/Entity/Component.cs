using System;
namespace GameEngine
{
	public abstract class Component
	{
		public Component()
		{
			m_shouldBeDestroyed = false;
		}

		public void Destroy()
		{
			m_shouldBeDestroyed = true;
		}

		public bool ShouldBeDestroyed
		{
			get
			{
				return m_shouldBeDestroyed;
			}
		}

		private bool m_shouldBeDestroyed;
	}
}
