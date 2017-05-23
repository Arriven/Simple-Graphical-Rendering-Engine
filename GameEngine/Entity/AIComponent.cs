using System;
namespace GameEngine
{
	public class AIComponent : Component
	{
		public AIComponent(GameEntity entity)
		{
			m_entity = entity;
		}

		public void Update(float dt)
		{
			if (ShouldBeDestroyed)
			{
				return;
			}
			float minDist = float.MaxValue;
			Vector targetImpulse = new Vector(0, 0);
			foreach (var physBody in m_entity.World.Physics.Bodies)
			{
                if(physBody == m_entity.Physics)
                {
                    continue;
                }
				if ((physBody.Position - m_entity.Physics.Position).LengthSquared() < minDist)
				{
					minDist = (physBody.Position - m_entity.Physics.Position).LengthSquared();
					targetImpulse = physBody.Position - m_entity.Physics.Position;
				}
			}
			m_entity.Physics.ApplyLinearImpulse(targetImpulse);
		}

		private GameEntity m_entity;
	}
}
