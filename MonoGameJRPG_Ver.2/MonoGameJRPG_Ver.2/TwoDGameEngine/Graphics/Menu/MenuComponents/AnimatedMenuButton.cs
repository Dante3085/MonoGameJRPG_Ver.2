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

        /// <summary>
        /// The functionality of the button.
        /// </summary>
        private Action _buttonFunctionality;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public override Rectangle Rectangle => _animSprite.BoundingBox;

        #endregion

        public AnimatedMenuButton(AnimatedSprite animSprite, Action functionality = null) : base(animSprite)
        {
            _buttonFunctionality = functionality;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MouseHoverReaction();

            if (OnLeftMouseClick())
                ExecuteFunctionality();
        }

        public override void ExecuteFunctionality()
        {
            _buttonFunctionality();
        }

        public override void ChangeFunctionality(Action functionality)
        {
            _buttonFunctionality = functionality;
        }

        public override void MouseHoverReaction()
        {
            _animSprite.PlayAnimation(IsMouseHover() ? EAnimation.MouseHover : EAnimation.Idle);
        }
    }
}
