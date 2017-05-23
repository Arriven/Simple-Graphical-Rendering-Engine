using System;
namespace GameEngine
{
    public class PhysicsComponent : Component
    {
        public PhysicsComponent()
        {

        }

        public void Update(float dt)
        {
            m_prevPos = m_position;
            m_position += m_impulse / m_mass * dt;
            m_hitbox.SetPosition(m_position);
            m_impulse *= 1 - m_friction;
        }

        public void RemoveUpdate()
        {
            m_position = m_prevPos;
            m_hitbox.SetPosition(m_position);
        }

        public void ApplyLinearImpulse(Vector impulse)
        {
            m_impulse += impulse;
        }

        public void ResetImpulse()
        {
            m_impulse = new Vector(0, 0);
        }

        public Vector Impulse
        {
            get
            {
                return m_impulse;
            }
        }

        public void SetPosition(Vector Position)
        {
            m_position = Position;
        }

        public HitBox HitBox
        {
            get
            {
                return m_hitbox;
            }
            set
            {
                m_hitbox = value;
            }
        }

        public Vector Position
        {
            get
            {
                return m_position;
            }
        }

        public void SetMass(float mass)
        {
            m_mass = mass;
        }

        public void SetFriction(float friction)
        {
            m_friction = friction;
        }

        private float m_mass = 0;
        private Vector m_impulse;
        private Vector m_position;
        private HitBox m_hitbox;
        private Vector m_prevPos;
        private float m_friction;
    }
}