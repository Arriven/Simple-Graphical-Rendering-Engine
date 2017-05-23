using System;
namespace GameEngine
{
	public class GameEntity
	{
		public GameEntity(GameWorld world)
		{
			m_world = world;
		}

		public PhysicsComponent Physics
		{
			get
			{
				return m_physics;
			}
			set
			{
				if (m_physics != null)
				{
					m_physics.Destroy();
				}
				m_world.Physics.AddBody(value);
				m_physics = value;
			}
		}

		public RenderComponent Render
		{
			get
			{
				return m_render;
			}
			set
			{
				if (m_render != null)
				{
					m_render.Destroy();
				}
				m_world.AddRenderable(value);
				m_render = value;
			}
		}

		public AIComponent AI
		{
			get
			{
				return m_AI;
			}
			set
			{
				if (m_AI != null)
				{
					m_AI.Destroy();
				}
				m_world.AddAI(value);
				m_AI = value;
			}
		}

		public GameWorld World
		{
			get
			{
				return m_world;
			}
		}

		public void Destroy()
		{
			m_AI.Destroy();
			m_render.Destroy();
			m_physics.Destroy();
		}

		private PhysicsComponent m_physics;
		private RenderComponent m_render;
		private AIComponent m_AI;
		private GameWorld m_world;
	}
}
