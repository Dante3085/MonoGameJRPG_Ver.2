using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic
{
    public interface IDrawable : IEntity
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
