using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts
{
    public class HBox : Layout
    {
        #region MemberVariables
        private int _x;
        private int _y;
        private int _horizontalOffset;
        private List<MenuElement> _elements = new List<MenuElement>();
        private Action _functionality;
        #endregion

        #region Properties
        public override int Width => WidthWidestElement();
        public override int Height => HeightTallestElement();
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
        public override int Offset
        {
            get => _horizontalOffset;
            set => _horizontalOffset = value;
        }
        #endregion

        public HBox(int x = 0, int y = 0, int horizontalOffset = 0, Action functionality = null, params MenuElement[] elements)
        {
            _x = x;
            _y = y;
            _horizontalOffset = horizontalOffset;
            _functionality = functionality;

            if (elements != null)
            {
                _elements.AddRange(elements);
                OrderElements();
            }
            else
            {
                _elements = new List<MenuElement>();
            }
        }

        private int HeightTallestElement()
        {
            if (_elements.Count == 0)
                return -1;

            int height = _elements[0].Height;

            foreach (MenuElement m in _elements)
                if (m.Height > height)
                    height = m.Height;
            return height;
        }

        private int WidthWidestElement()
        {
            if (_elements.Count == 0)
                return -1;

            int width = _elements[0].Width;

            foreach (MenuElement m in _elements)
                if (m.Width > width)
                    width = m.Width;
            return width;
        }

        /// <summary>
        /// Positions elements horizontally.
        /// Position of element is dependant on position of element in element list.
        /// </summary>
        public override void OrderElements()
        {
            if (_elements.Count == 0)
                return;

            // Position first element at upper left corner of VBox.
            _elements[0].X = this._x;
            _elements[0].Y = this._y;

            if (_elements.Count == 1)
                return;

            // Position every element (except first) at VBox.Y and VBox.X + previousElement.X + horizontalOffset.
            // Makes it so that elements aren't stacked on top of each other and variable spacing is possible.
            for (int i = 1; i < _elements.Count; i++)
            {
                _elements[i].Y = this._y;
                _elements[i].X = _elements[i - 1].X + _elements[i - 1].Width + _horizontalOffset;
            }
        }

        public override List<MenuElement> Elements()
        {
            return _elements;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (MenuElement m in _elements)
                m.Update(gameTime);
            OrderElements();
        }

        public override void ExecuteFunctionality()
        {
            _functionality();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            _functionality = functionality;
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            foreach (MenuElement m in _elements)
                m.Render(spriteBatch);
        }
    }
}
