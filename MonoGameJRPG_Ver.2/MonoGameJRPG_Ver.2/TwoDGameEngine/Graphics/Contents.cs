using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine
{
    /// <summary>
    /// Holds Textures and SpriteFonts for loading and usage.
    /// </summary>
    public static class Contents
    {
        #region Texture2Ds
    
        #region BackgroundImages

        public static Texture2D space;
        public static Texture2D samurai;
        public static Texture2D blueBackground;

        #endregion
        #region MenuComponents

        public static Texture2D redButtonNoHover;
        public static Texture2D redButtonHover;
        public static Texture2D btnNewGame;
        public static Texture2D glowingButton;

        #endregion
        #region Characters

        public static Texture2D warrior;
        public static Texture2D bowlingBall;

        #endregion
        #region Other

        public static Texture2D rectangleTex;

        #endregion

        #endregion
        #region SpriteFonts

        public static SpriteFont arial12;
        public static SpriteFont arial15;

        #endregion

        /// <summary>
        /// Uses given ContentManager and GraphicsDevice to load all Content declared inside Contents class.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadAll(ContentManager c, GraphicsDevice g)
        {
            // space = c.Load<Texture2D>("");
            // samurai = c.Load<Texture2D>("");
            // blueBackground = c.Load<Texture2D>("");

            redButtonNoHover = c.Load<Texture2D>("MenuComponents/RedButtonNoHover");
            redButtonHover = c.Load<Texture2D>("MenuComponents/RedButtonHover");
            btnNewGame = c.Load<Texture2D>("MenuComponents/NewGameButton");
            glowingButton = c.Load<Texture2D>("MenuComponents/GlowingButton");

            warrior = c.Load<Texture2D>("Characters/warrior");
            bowlingBall = c.Load<Texture2D>("Characters/BowlingBall");

            rectangleTex = new Texture2D(g, 1, 1, false, SurfaceFormat.Color);
            rectangleTex.SetData(new[] { Color.White });

            arial12 = c.Load<SpriteFont>("SpriteFonts/Arial12");
            arial15 = c.Load<SpriteFont>("SpriteFonts/Arial15");
        }

        /// <summary>
        /// Uses given ContentManager to load all Character related Textures.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadCharacters(ContentManager c)
        {
            warrior = c.Load<Texture2D>("Characters/warrior");
        }
    }
}