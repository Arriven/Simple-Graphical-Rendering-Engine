using System;
namespace GameEngine
{
	public class RenderComponent : Component
	{
		public RenderComponent(GameEntity entity)
		{
			m_entity = entity;
		}

		public void SetTexture(ICanvas texture)
		{
			m_texture = texture;
		}

		public void Update(float dt)
		{
			Position = m_entity.Physics.Position;
		}

		public Vector Size
		{
			get
			{
				return m_size;
			}
			set
			{
				m_size = value;
			}
		}

		public Vector Position
		{
			get
			{
				return m_position;
			}
			set
			{
				m_position = value;
			}
		}

		public EShape Shape
		{
			get
			{
				return m_shape;
			}
			set
			{
				m_shape = value;
			}
		}

		public void Render(ICanvas canvas)
		{
			for (float x = -1; x <= 2; x += 2 / (float)canvas.Width)
			{
				for (float y = -1; y <= 1; y += 2 / (float)canvas.Height)
				{
					if (Dispose(x, y))
					{
						continue;
					}
					canvas[x, y] = GetPixel(x, y);
				}
			}
		}

		private bool Dispose(float x, float y)
		{
			if (m_shape == EShape.None)
			{
				return true;
			}

			if (x > m_position.x + m_size.x / 2 || x < m_position.x - m_size.x / 2 ||
				y > m_position.y + m_size.y / 2 || y < m_position.y - m_size.y / 2)
			{
				return true;
			}

			if (m_shape == EShape.Circle)
			{
				if (Math.Pow((x - m_position.x) / m_size.x / 2, 2) +
				   Math.Pow((y - m_position.y) / m_size.y / 2, 2) > 1)
				{
					return true;
				}
			}

			return false;
		}

		private Color GetPixel(float x, float y)
		{
            if(m_texture != null)
			return m_texture[(x - m_position.x) / m_size.x, (y - m_position.y) / m_size.y];
            Color color = new Color();
            color.A = 255;
            return color;
		}

		private EShape m_shape;
		private Vector m_size;
		private Vector m_position;
		private ICanvas m_texture;
		private GameEntity m_entity;
	}
}
