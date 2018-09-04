using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public class CollisionManager
    {
        private List<ICollidable> _collidables = new List<ICollidable>();

        public CollisionManager(params ICollidable[] collidables)
        {
            _collidables.AddRange(collidables);
        }

        /// <summary>
        /// Checks for every Collidable in this CollisionManager if it collides with any of the other Collidables.
        /// If collision happened, HandleCollision is called.
        /// </summary>
        public void CheckCollisions()
        {
            foreach (ICollidable c1 in _collidables)
                foreach (ICollidable c2 in _collidables)
                if (c1.CollidesWith(c2))
                {
                    Game1.gameConsole.Log("Helau");
                    c1.HandleCollision(c2);
                }
        }
    }
}
