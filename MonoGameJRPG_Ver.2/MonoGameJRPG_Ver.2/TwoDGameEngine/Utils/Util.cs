using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Utils
{
    public static class Util
    {
        #region Methods

        /// <summary>
        /// Draws a Rectangle outline (unfilled Rectangle) using the given Rectangle object. <para></para>
        /// "lines" parameter has to be exactly 4 Rectangle objects.
        /// "lines" parameter exists to avoid 4 additional Rectangle objects from being created everytime this method is called.
        /// This is important since this method is called a lot (May prevent GarbageCollection hickups).
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="rec"></param>
        /// <param name="lines"></param>
        /// <param name="texture"></param>
        /// <param name="color"></param>
        public static void DrawRectangleOutline(Rectangle rec, Rectangle[] lines, Texture2D texture, Color color, SpriteBatch spriteBatch)
        {
            if (lines.Length != 4)
                throw new ArgumentException("'lines' has to bee an array of exactly 4 Rectangle objects!");

            // left
            lines[0].X = rec.X;
            lines[0].Y = rec.Y;
            lines[0].Width = 2;
            lines[0].Height = rec.Height;

            // top
            lines[1].X = rec.X;
            lines[1].Y = rec.Y;
            lines[1].Width = rec.Width;
            lines[1].Height = 2;

            // right
            lines[2].X = rec.X + rec.Width;
            lines[2].Y = rec.Y;
            lines[2].Width = 2;
            lines[2].Height = rec.Height;

            // bottom
            lines[3].X = rec.X;
            lines[3].Y = rec.Y + rec.Height;
            lines[3].Width = rec.Width;
            lines[3].Height = 2;

            spriteBatch.Draw(texture, lines[0], color);
            spriteBatch.Draw(texture, lines[1], color);
            spriteBatch.Draw(texture, lines[2], color);
            spriteBatch.Draw(texture, lines[3], color);
        }
        /// <summary>
        /// Draws a filled Rectangle using the given Rectangle object.
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="color"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="opacity"></param>
        public static void DrawRectangle(Rectangle rec, Color color, SpriteBatch spriteBatch, double opacity = 1.0)
        {
            Texture2D recTex = new Texture2D(Game1.graphics.GraphicsDevice, rec.Width, rec.Height);
            Color[] data = new Color[rec.Width * rec.Height];
            Vector2 pos = new Vector2(rec.Location.X, rec.Location.Y);

            for (int i = 0; i < data.Length; i++)
                data[i] = color;
            recTex.SetData(data);

            spriteBatch.Draw(recTex, pos, Color.White * (float)opacity);
        }

        /// <summary>
        /// Draws a circle with the given radius, color, sharpness and opacity at the given position.
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="color"></param>
        /// <param name="pos"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="sharpness"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Texture2D DrawCircle(int radius, Color color, Vector2 pos, SpriteBatch spriteBatch, double sharpness = 1.0, double opacity = 1.0)
        {
            int diameter = radius * 2;
            Texture2D circleTex = new Texture2D(Game1.graphics.GraphicsDevice, diameter, diameter, false, SurfaceFormat.Color);
            Color[] colorData = new Color[circleTex.Width * circleTex.Height];
            Vector2 center = new Vector2(radius);

            for (int colIndex = 0; colIndex < circleTex.Width; colIndex++)
            {
                for (int rowIndex = 0; rowIndex < circleTex.Height; rowIndex++)
                {
                    Vector2 position = new Vector2(colIndex, rowIndex);
                    float distance = Vector2.Distance(center, position);

                    // hermite iterpolation
                    float x = distance / diameter;
                    float edge0 = (radius * (float)sharpness) / (float)diameter;
                    float edge1 = radius / (float)diameter;
                    float temp = MathHelper.Clamp((x - edge0) / (edge1 - edge0), 0.0f, 1.0f);
                    float result = temp * temp * (3.0f - 2.0f * temp);

                    colorData[rowIndex * circleTex.Width + colIndex] = color * (1f - result);
                }
            }
            circleTex.SetData<Color>(colorData);

            spriteBatch.Draw(circleTex, pos, color * (float)opacity);

            return circleTex;
        }

        /// <summary>
        /// Appends the given string to a pre-defined Logfile (Textfile).
        /// If no logfile exists, it is automatically created.
        /// </summary>
        public static void Log(string text)
        {
            if (!File.Exists("log.txt"))
                File.WriteAllText("log.txt", text);
            else
                using (StreamWriter file = new StreamWriter("log.txt"))
                    file.WriteLine(text);
        }

        #endregion
    }
}
