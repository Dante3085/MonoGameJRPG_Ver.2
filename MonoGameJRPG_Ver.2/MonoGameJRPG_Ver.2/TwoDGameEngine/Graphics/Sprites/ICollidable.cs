using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public interface ICollidable
    {
        string Name { get; }
        Rectangle BoundingBox { get; }
        bool CollidesWith(ICollidable partner);
    }
}