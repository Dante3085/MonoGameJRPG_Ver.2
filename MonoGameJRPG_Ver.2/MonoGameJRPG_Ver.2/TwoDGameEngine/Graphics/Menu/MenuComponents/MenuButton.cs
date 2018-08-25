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

        private Action _buttonFunctionality;
        #endregion

        #region Properties
        public override int X
        {
            get => _buttonRec.X;
            set => _buttonRec.X = value;
        }

        public override int Y
        {
            get => _buttonRec.Y;
            set => _buttonRec.Y = value;
        }

        public override int Width => _buttonRec.Width;
        public override int Height => _buttonRec.Height;

        public override Rectangle Rectangle => _buttonRec;

        #endregion

        public MenuButton(Texture2D buttonTextureNoHover, Texture2D buttonTextureHover, int x = 0, int y = 0, Action function = null)
        {
            _buttonTextureNoHover = buttonTextureNoHover;
            _buttonTextureHover = buttonTextureHover;
            _buttonFunctionality = function;

            _activeButtonTexture = buttonTextureNoHover;

            _buttonRec = new Rectangle(x, y, _activeButtonTexture.Width, _activeButtonTexture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_activeButtonTexture, _buttonRec, Color.White);

            if (drawRec)
                Util.DrawRectangle(spriteBatch, _buttonRec, _buttonRecLines, Contents.rectangleTex, Color.Blue);
        }

        public override void ExecuteFunctionality()
        {
            _buttonFunctionality();
        }

        /// <summary>
        /// Changes MenuButton's functionality to the given one.
        /// </summary>
        /// <param name="functionality"></param>
        public override void ChangeFunctionality(Action functionality)
        {
            _buttonFunctionality = functionality;
        }

        public override void Update(GameTime gameTime)
        {
            MouseHoverReaction();

            if (OnLeftMouseClick())
                ExecuteFunctionality();
        }

        /// <summary>
        /// Changes activeButtonTexture on MouseHover.
        /// </summary>
        public override void MouseHoverReaction()
        {
            _activeButtonTexture = IsMouseHover() ? _buttonTextureHover : _buttonTextureNoHover;
        }
    }
}
