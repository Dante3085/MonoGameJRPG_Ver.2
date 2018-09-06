using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class MenuButton2 : MenuElement
    {
        #region MemberVariables

        private Text _text;
        private Rectangle _rec;

        #endregion

        #region Properties

        public override int Width { get; }
        public override int Height { get; }
        public override Rectangle Rectangle { get; }

        #endregion

        public MenuButton2(string name, string text, Rectangle rec, Action functionality) 
            : base(name, rec.X, rec.Y, functionality)
        {
            _text = new Text(text, rec.X, rec.Y, text);
            _rec = new Rectangle(rec.X, rec.Y, _text.Width, _text.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _text.Update(gameTime);
            _rec.Width = _text.Width;
            _rec.Height = _text.Height;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _text.Draw(spriteBatch);
            Util.DrawRectangle(_rec, Color.DarkRed, spriteBatch, 0.5);
        }

        public override void MouseHoverReaction()
        {
            // throw new NotImplementedException();
        }

        public override void CursorReaction(GameTime gameTime)
        {
            // throw new NotImplementedException();
        }
    }
}
