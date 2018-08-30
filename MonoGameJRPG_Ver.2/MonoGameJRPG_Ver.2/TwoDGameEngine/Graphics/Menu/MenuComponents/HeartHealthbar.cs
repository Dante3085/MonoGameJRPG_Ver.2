using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class HeartHealthbar : MenuElement
    {
        #region MemberVariables

        private Character _character;
        private HBox _box;

        #endregion

        #region Properties

        public override int Width => _box.Width;
        public override int Height => _box.Height;

        public override Rectangle Rectangle => _box.Rectangle;

        #endregion

        public HeartHealthbar(string name, int x, int y, Action functionality, Character character) 
            : base(name, x, y, functionality)
        {
            _character = character;

            MenuElement[] hearts = new MenuElement[character.MaxHp];
            for (int i = 0; i < hearts.Length; i++)
                hearts[i] = new AnimatedMenuButton("heart" + i, SpriteFactory.Heart(Vector2.Zero), x, y, () => Game1.gameConsole.Log("Heart clicked"));

            _box = new HBox("heartbox", x, y, null, 0, hearts);
        }

        public override void Update(GameTime gameTime)
        {
            _box.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _box.Draw(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            throw new NotImplementedException();
        }

        public override void CursorReaction()
        {
            throw new NotImplementedException();
        }
    }
}
