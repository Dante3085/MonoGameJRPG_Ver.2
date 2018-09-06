using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class CharacterInfo : MenuElement
    {
        #region MemberVariables

        private Character _character;
        private HBox _hBox;
        private Text _name;
        private Text _maxHp;
        private Text _currentHp;

        #endregion

        #region Properties

        public override int Width => _hBox.Width;
        public override int Height => _hBox.Height;
        public override Rectangle Rectangle => _hBox.Rectangle;

        #endregion

        #region Methods

        public CharacterInfo(string name, Character character, int x = 0, int y = 0, Action functionality = null) 
            : base(name, x, y, functionality)
        {
            _character = character;
            _name = new Text("name", text: _character.Name);
            _maxHp = new Text("maxHp", text: "" + _character.MaxHp);
            _currentHp = new Text("currentHp", text: "" + _character.CurrentHp);
            _hBox = new HBox("hbox", horizontalOffset: 5, elements: new MenuElement[]
            {
                _name, _maxHp, _currentHp
            });
        }

        public override void Update(GameTime gameTime)
        {
            _maxHp.SetText("" + _character.MaxHp);
            _currentHp.SetText("" + _character.CurrentHp);
            _hBox.Update(gameTime);
            _hBox.X = _x;
            _hBox.Y = _y;

            //_name.CursorOnIt = false;
            //_maxHp.CursorOnIt = false;
            //_currentHp.CursorOnIt = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _hBox.Draw(spriteBatch);
        }

        public override void MouseHoverReaction()
        {
            throw new NotImplementedException();
        }

        public override void CursorReaction(GameTime gameTime)
        {
            if (_cursorOnIt)
            {
                _name.CursorOnIt = true;
                _maxHp.CursorOnIt = true;
                _currentHp.CursorOnIt = true;
            }
        }

        #endregion
    }
}
