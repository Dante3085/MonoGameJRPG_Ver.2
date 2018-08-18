using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class AnimatedMenuButton : MenuElement
    {
        #region MemberVariables

        private AnimatedSprite animSprite;

        #endregion
        #region Properties

        public override int Width { get; }
        public override int Height { get; }
        public override int X { get; set; }
        public override int Y { get; set; }

        #endregion

        public AnimatedMenuButton()
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteFunctionality()
        {
            throw new NotImplementedException();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            throw new NotImplementedException();
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
