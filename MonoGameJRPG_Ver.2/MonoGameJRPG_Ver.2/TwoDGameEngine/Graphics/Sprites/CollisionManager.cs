using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public class CollisionManager : IEntity
    {
        private List<ICollidable> _collidables;

        public List<ICollidable> Collidables => _collidables;

        public CollisionManager(params ICollidable[] collidables)
        {
            _collidables = new List<ICollidable>(collidables);
        }

        /// <summary>
        /// Checks for every Collidable in this CollisionManager if it collides with any of the other Collidables.
        /// If collision happened, HandleCollision is called.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            foreach (ICollidable c1 in _collidables)
                foreach (ICollidable c2 in _collidables)
                if (c1.CollidesWith(c2))
                    c1.HandleCollision(c2);
        }
    }
}
