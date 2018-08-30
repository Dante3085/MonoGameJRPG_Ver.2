using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    /// <summary>
    /// MenuButton that uses an AnimatedSprite to animate itself.
    /// </summary>
    public class AnimatedMenuButton : AnimatedMenuElement
    {
        #region MemberVariables

        #endregion

        #region Properties

        public override Rectangle Rectangle => _animSprite.BoundingBox;

        #endregion

        public AnimatedMenuButton(string name, AnimatedSprite animSprite, int x = 0, int y = 0, Action functionality = null) 
            : base(name, animSprite, x, y, functionality)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (OnLeftMouseClick())
                ExecuteFunctionality();
        }

        public override void MouseHoverReaction()
        {
            _animSprite.PlayAnimation(IsMouseHover() ? EAnimation.MouseHover : EAnimation.Idle);
            if (IsMouseHover())
                _animSprite.PlayAnimation(EAnimation.MouseHover);
            else if ((!IsMouseHover()) && (!_cursorOnIt))
                _animSprite.PlayAnimation(EAnimation.Idle);
        }

        public override void CursorReaction()
        {
            if (_cursorOnIt)
                _animSprite.PlayAnimation(EAnimation.MouseHover);
        }
    }
}
