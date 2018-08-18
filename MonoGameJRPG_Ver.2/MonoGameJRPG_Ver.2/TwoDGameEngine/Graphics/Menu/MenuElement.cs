using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu
{
    /// <summary>
    /// MenuElements can be added to a Menu.
    /// </summary>
    public abstract class MenuElement
    {
        #region Properties
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        #endregion
        #region AbstractMethods
        /// <summary>
        /// Updates the MenuElement.
        /// </summary>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Executes MenuElement's functionality.
        /// </summary>
        public abstract void ExecuteFunctionality();

        /// <summary>
        /// Changes MenuElements functionality to given functionality.
        /// </summary>
        /// <param name="functionality"></param>
        public abstract void ChangeFunctionality(Action functionality);

        /// <summary>
        /// Draws the MenuElement on screen with use of SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Render(SpriteBatch spriteBatch);
        #endregion
    }
}
