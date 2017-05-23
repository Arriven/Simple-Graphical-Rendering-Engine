using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class EntityFactory
    {
        public EntityFactory(GameWorld world)
        {
            m_world = world;
            m_physicsFactory = new PhysicsFactory();
        }

        public GameEntity Create(Vector Position, Vector Size, float mass = 1, float friction = 0.15f, EShape shape = EShape.Square, ICanvas texture = null)
        {
            GameEntity result = new GameEntity(m_world);
            result.Physics = m_physicsFactory.Create(Position, Size, mass, friction, shape);

            result.Render = new RenderComponent(result);
            result.Render.SetTexture(texture);
            result.Render.Shape = shape;
            result.Render.Position = Position;
            result.Render.Size = Size;

            result.AI = new AIComponent(result);

            return result;
        }

        private GameWorld m_world;
        private PhysicsFactory m_physicsFactory;

        class PhysicsFactory
        {
            public PhysicsFactory()
            {
                
            }

            public PhysicsComponent Create(Vector Position, Vector Size, float mass, float friction, EShape shape)
            {
                PhysicsComponent result = new PhysicsComponent();
                result.SetPosition(Position);
                result.SetMass(mass);
                result.HitBox = new Rectangle(Position, Size);


                return result;
            }
        }
    }
}
