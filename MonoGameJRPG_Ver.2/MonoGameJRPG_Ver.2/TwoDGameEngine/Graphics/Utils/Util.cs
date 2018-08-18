using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Utils
{
    public static class Util
    {
        /// <summary>
        /// Draws outline of passed Rectangle object.
        /// lines parameter has to be exactly 4 Rectangle objects.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="rectangle"></param>
        /// <param name="lines"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Rectangle[] lines, Texture2D texture, Color color)
        {
            if (lines.Length != 4)
                throw new ArgumentException("'lines' has to bee an array of exactly 4 Rectangle objects!");

            // left
            lines[0].X = rectangle.X;
            lines[0].Y = rectangle.Y;
            lines[0].Width = 2;
            lines[0].Height = rectangle.Height;

            // top
            lines[1].X = rectangle.X;
            lines[1].Y = rectangle.Y;
            lines[1].Width = rectangle.Width;
            lines[1].Height = 2;

            // right
            lines[2].X = rectangle.X + rectangle.Width;
            lines[2].Y = rectangle.Y;
            lines[2].Width = 2;
            lines[2].Height = rectangle.Height;

            // bottom
            lines[3].X = rectangle.X;
            lines[3].Y = rectangle.Y + rectangle.Height;
            lines[3].Width = rectangle.Width;
            lines[3].Height = 2;

            spriteBatch.Draw(texture, lines[0], color);
            spriteBatch.Draw(texture, lines[1], color);
            spriteBatch.Draw(texture, lines[2], color);
            spriteBatch.Draw(texture, lines[3], color);
        }
    }
}
