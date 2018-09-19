using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    /// <summary>
    /// A SceneState is a State that displays visible information, meaning that it has a
    /// backgroundTexture and other visible elements.
    /// </summary>
    public class SceneState : State
    {
        /// <summary>
        /// BackgroundTexture for the Scene.
        /// </summary>
        private Texture2D _background;

        /// <summary>
        /// BackgroundRectangle for defining the bounds of the BackgroundTexture.
        /// </summary>
        private Rectangle _backgroundRec;

        public SceneState(Texture2D background, List<IEntity> entities, List<EState> next, 
           Action keyboardHandler = null, Action gamePadHandler = null, string name = "NO_NAME_SCENE_STATE") 
            : base(entities, next, keyboardHandler, gamePadHandler, name)
        {
            _background = background;
            _backgroundRec = new Rectangle(0, 0, Game1.screenWidth, Game1.screenHeight);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

        }

        /// <summary>
        /// Draws backgroundTexture then calls Draw() of base class.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, _backgroundRec, Color.White);
            base.Draw(spriteBatch);
        }

        /// <summary>
        /// TODO
        /// </summary>
        public override void OnEnter()
        {
            base.OnEnter();
        }

        /// <summary>
        /// TODO
        /// </summary>
        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
