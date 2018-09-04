using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    /// <summary>
    /// Marks Types for being able to collide with each other.
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// Identify ICollidables that took part in the collision.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// BoundingBox that is necessary for detecting the collision.
        /// </summary>
        Rectangle BoundingBox { get; }

        /// <summary>
        /// Contains logic for deciding if collision happened.
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        bool CollidesWith(ICollidable partner);

        /// <summary>
        /// Called in case of Collision.
        /// </summary>
        /// <param name="parnter"></param>
        void HandleCollision(ICollidable parnter);
    }
}