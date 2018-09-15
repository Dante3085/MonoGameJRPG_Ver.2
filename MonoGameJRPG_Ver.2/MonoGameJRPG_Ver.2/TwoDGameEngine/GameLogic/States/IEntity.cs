using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public interface IEntity
    {
        void Update(GameTime gameTime);
    }
}
