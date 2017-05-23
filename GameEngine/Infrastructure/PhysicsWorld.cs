using System;
using System.Collections.Generic;

namespace GameEngine
{
	public class PhysicsWorld
	{
		public PhysicsWorld()
		{
			m_physicsBodies = new List<PhysicsComponent>();
		}

		public void Update(float dt)
		{
			m_timer += dt;
            if(m_timer > 1)
            {
                m_timer = 0;
            }
			while (m_timer >= m_step)
			{
				m_timer -= m_step;
				MakeStep();
			}
		}

		public List<PhysicsComponent> Bodies
		{
			get
			{
				return m_physicsBodies;
			}
		}

		public void CleanUp()
		{
			m_physicsBodies.RemoveAll((PhysicsComponent obj) => obj.ShouldBeDestroyed);
		}

		public void AddBody(PhysicsComponent body)
		{
			m_physicsBodies.Add(body);
		}

		private void MakeStep()
		{
            Dictionary<PhysicsComponent, Vector> hitImpulses = new Dictionary<PhysicsComponent, Vector>();
			foreach (var body in m_physicsBodies)
			{
				body.Update(m_step);
			}
            foreach (var body in m_physicsBodies)
            {
                hitImpulses[body] = new Vector(0, 0);
                bool collides = false;
                foreach(var other in m_physicsBodies)
                {
                    if(other != body && other.HitBox.Intersects(body.HitBox))
                    {
                        collides = true;
                        Vector hitImpulse = (body.Position - other.Position).Normalized() * other.Impulse.Length();
                        hitImpulses[body] += hitImpulse;
                        break;
                    }
                }
            }
            foreach (var body in m_physicsBodies)
            {
                ResolveCollisionWithBorders(body, hitImpulses);
                if (hitImpulses[body].LengthSquared() > 0)
                {
                    body.RemoveUpdate();
                    body.ResetImpulse();
                    body.ApplyLinearImpulse(hitImpulses[body]);
                }
            }

        }

        private void ResolveCollisionWithBorders(PhysicsComponent body, Dictionary<PhysicsComponent, Vector> hitImpulses)
        {
            if(body.HitBox.Intersects(new Rectangle(new Vector(2,0), new Vector(2,2))) && body.Impulse.x > 0)
            {
                hitImpulses[body] += new Vector(-body.Impulse.x, body.Impulse.y);
            }
            if (body.HitBox.Intersects(new Rectangle(new Vector(-2, 0), new Vector(2, 2))) && body.Impulse.x < 0)
            {
                hitImpulses[body] += new Vector(-body.Impulse.x, body.Impulse.y);
            }
            if (body.HitBox.Intersects(new Rectangle(new Vector(0, 2), new Vector(2, 2))) && body.Impulse.y > 0)
            {
                hitImpulses[body] += new Vector(body.Impulse.x, -body.Impulse.y);
            }
            if (body.HitBox.Intersects(new Rectangle(new Vector(0, -2), new Vector(2, 2))) && body.Impulse.y < 0)
            {
                hitImpulses[body] += new Vector(body.Impulse.x, -body.Impulse.y);
            }
        }

		private List<PhysicsComponent> m_physicsBodies;
		private float m_timer = 0;
		private const float m_step = 1 / 300.0f;
	}
}
