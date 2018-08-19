using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics
{
    public class Text : MenuElement
    {
        #region StaticVariables

        public static bool drawTexRec = false;

        #endregion

        #region MemberVariables

        private string _text;
        private SpriteFont _activeSpriteFont;
        private SpriteFont _spriteFontNoHover;
        private SpriteFont _spriteFontHover;
        private Vector2 _textSize;
        private int _x;
        private int _y;

        private Action _functionality;

        #region TextRectangle

        private Rectangle _textRec;
        private Rectangle[] _textRecLines = {new Rectangle(), new Rectangle(), new Rectangle(), new Rectangle()};
        public bool _drawTextRec;

        #endregion


        #endregion
        #region Properties

        public override int X
        {
            get => _x;
            set => _x = value;
        }

        public override int Y
        {
            get => _y;
            set => _y = value;
        }

        public override int Width => _textRec.Width;
        public override int Height => _textRec.Height;
        public override Rectangle Rectangle => _textRec;

        #endregion

        public Text(SpriteFont fontNoHover, SpriteFont fontHover, string text = "", int x = 0, int y = 0, Action functionality = null)
        {
            _text = text;
            _x = x;
            _y = y;
            _functionality = functionality;
            _spriteFontNoHover = fontNoHover;
            _spriteFontHover = fontHover;
            _activeSpriteFont = _spriteFontNoHover;

            _textSize = _activeSpriteFont.MeasureString(_text);
            _textRec = new Rectangle(x, y, (int)_textSize.X, (int)_textSize.Y);
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public override void Update(GameTime gameTime)
        {
            _textSize = _activeSpriteFont.MeasureString(_text);

            // Update _textRec x,y, Position
            _textRec.X = (int)_x;
            _textRec.Y = (int)_y;

            // Update _textRec size.
            _textRec.Width = (int)_textSize.X;
            _textRec.Height = (int) _textSize.Y;

            MouseHoverReaction();

            if (OnLeftMouseClick())
                ExecuteFunctionality();
        }

        public override void ExecuteFunctionality()
        {
            _functionality();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            _functionality = functionality;
        }

        /// <summary>
        /// Handles Reactions that MenuButton will have on MouseHover.
        /// </summary>
        public override void MouseHoverReaction()
        {
            _activeSpriteFont = IsMouseHover() ? _spriteFontHover : _spriteFontNoHover;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_activeSpriteFont, _text, new Vector2(_x, _y), Color.Black);

            if (drawTexRec)
                Util.DrawRectangle(spriteBatch, _textRec, _textRecLines, Contents.rectangleTex, Color.Red);
        }
    }
}
