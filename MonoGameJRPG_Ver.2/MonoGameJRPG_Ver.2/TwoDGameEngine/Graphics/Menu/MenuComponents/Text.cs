using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics
{
    /// <summary>
    /// Interactive and locatable Text, primarily used in Menus.
    /// </summary>
    public class Text : MenuElement
    {
        #region MemberVariables

        public static bool drawTexRec = false;

        private string _text;
        private SpriteFont _activeSpriteFont;
        private SpriteFont _spriteFontNoHover;
        private SpriteFont _spriteFontHover;
        private Vector2 _textSize;

        #region TextRectangle

        private Rectangle _textRec;
        private Rectangle[] _textRecLines = {new Rectangle(), new Rectangle(), new Rectangle(), new Rectangle()};
        public bool _drawTextRec;

        #endregion


        #endregion
        #region Properties

        public override int Width => _textRec.Width;
        public override int Height => _textRec.Height;
        public override Rectangle Rectangle => _textRec;

        #endregion
        #region Methods

        /// <summary>
        /// Constructs a Text with given fonts, text, position and functionality.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fontNoHover"></param>
        /// <param name="fontHover"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="functionality"></param>
        public Text(string name, SpriteFont fontNoHover, SpriteFont fontHover, int x = 0, int y = 0, string text = "", Action functionality = null) 
            : base(name, x, y, functionality)
        {
            _text = text;
            _spriteFontNoHover = fontNoHover;
            _spriteFontHover = fontHover;
            _activeSpriteFont = _spriteFontNoHover;

            _textSize = _activeSpriteFont.MeasureString(_text);
            _textRec = new Rectangle(_x, _y, (int)_textSize.X, (int)_textSize.Y);
        }

        /// <summary>
        /// Constructs a text with given text, position and functionality.
        /// Fonts will be automatically set depending on screen size.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="functionality"></param>
        public Text(string name, int x = 0, int y = 0, string text = "", Action functionality = null) 
            : base(name, x, y, functionality)
        {
            if (Game1.screenWidth > 1920)
            {
                _spriteFontNoHover = Contents.arial30;
                _spriteFontHover = Contents.arial35;
                _activeSpriteFont = _spriteFontNoHover;
            }
            else
            {
                _spriteFontNoHover = Contents.arial18;
                _spriteFontHover = Contents.arial20;
                _activeSpriteFont = _spriteFontNoHover;
            }

            _text = text;

            _textSize = _activeSpriteFont.MeasureString(_text);
            _textRec = new Rectangle(_x, _y, (int)_textSize.X, (int)_textSize.Y);
        }

        public void SetText(string text)
        {
            _text = text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _textSize = _activeSpriteFont.MeasureString(_text);

            // Update _textRec x,y, Position
            _textRec.X = _x;
            _textRec.Y = _y;

            // Update _textRec size.
            _textRec.Width = (int)_textSize.X;
            _textRec.Height = (int) _textSize.Y;

            if (OnLeftMouseClick())
                ExecuteFunctionality();
        }

        public override void ExecuteFunctionality()
        {
            if (_functionality != null)
                _functionality();
        }

        /// <summary>
        /// Handles Reactions that MenuButton will have on MouseHover.
        /// </summary>
        public override void MouseHoverReaction()
        {
            _activeSpriteFont = IsMouseHover() ? _spriteFontHover : _spriteFontNoHover;
        }

        public override void CursorReaction()
        {
            if (_cursorOnIt)
                _activeSpriteFont = _spriteFontHover;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_activeSpriteFont, _text, new Vector2(_x, _y), Color.Black);

            if (drawTexRec)
                Util.DrawRectangle(spriteBatch, _textRec, _textRecLines, Contents.rectangleTex, Color.Red);
        }

        #endregion
    }
}
