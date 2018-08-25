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
        public override int X
        {
            get => _box.X;
            set => _box.X = value;
        }
        public override int Y
        {
            get => _box.Y;
            set => _box.Y = value;
        }

        public override Rectangle Rectangle => _box.Rectangle;

        #endregion

        public HeartHealthbar(Character character, int x, int y)
        {
            _character = character;

            MenuElement[] hearts = new MenuElement[character.MaxHp];
            for (int i = 0; i < hearts.Length; i++)
                hearts[i] = new AnimatedMenuButton(SpriteFactory.Heart(Vector2.Zero), () => Game1.gameConsole.Log("Heart clicked"));

            _box = new HBox(x, y, 0, null, hearts);
        }

        public override void Update(GameTime gameTime)
        {
            _box.Update(gameTime);
            

        }

        public override void ExecuteFunctionality()
        {
            throw new NotImplementedException();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _box.Draw(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            throw new NotImplementedException();
        }
    }
}
