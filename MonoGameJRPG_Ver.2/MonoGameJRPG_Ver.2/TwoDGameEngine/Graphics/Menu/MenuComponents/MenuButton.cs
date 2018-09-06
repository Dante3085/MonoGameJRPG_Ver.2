using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class MenuButton : MenuElement
    {
        public static bool drawRec = false;

        #region MemberVariables
        private Texture2D _buttonTextureNoHover;
        private Texture2D _buttonTextureHover;
        private Texture2D _activeButtonTexture;

        private Rectangle _buttonRec;
        private Rectangle[] _buttonRecLines = {new Rectangle(), new Rectangle(), new Rectangle(), new Rectangle(),};

        #endregion

        #region Properties

        public override int Width => _buttonRec.Width;
        public override int Height => _buttonRec.Height;

        public override Rectangle Rectangle => _buttonRec;

        #endregion

        public MenuButton(string name, Texture2D buttonTextureNoHover, Texture2D buttonTextureHover, int x = 0, int y = 0, Action functionality = null) : base(name, x, y, functionality)
        {
            _buttonTextureNoHover = buttonTextureNoHover;
            _buttonTextureHover = buttonTextureHover;

            _activeButtonTexture = buttonTextureNoHover;

            _buttonRec = new Rectangle(x, y, _activeButtonTexture.Width, _activeButtonTexture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_activeButtonTexture, _buttonRec, Color.White);

            if (drawRec)
                Util.DrawRectangleOutline(_buttonRec, _buttonRecLines, Contents.rectangleTex, Color.Blue, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (OnLeftMouseClick())
                ExecuteFunctionality();

            // TODO: Weiß nicht, ob alle Zuweisungen hier nötig sind. Einige aber schon. Sonst kann der Button im Layout nicht korrekt angebracht werden.
            _buttonRec.X = _x;
            _buttonRec.Y = _y;
            _buttonRec.Width = _activeButtonTexture.Width;
            _buttonRec.Height = _activeButtonTexture.Height;
        }

        /// <summary>
        /// Changes activeButtonTexture on MouseHover.
        /// </summary>
        public override void MouseHoverReaction()
        {
            _activeButtonTexture = IsMouseHover() ? _buttonTextureHover : _buttonTextureNoHover;
        }

        public override void CursorReaction(GameTime gameTime)
        {
            if (_cursorOnIt)
                _activeButtonTexture = _buttonTextureHover;
        }
    }
}
