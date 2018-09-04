using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine
{
    /// <summary>
    /// Utility class for storing and loading Textures and SpriteFonts.
    /// </summary>
    public static class Contents
    {
        #region Texture2Ds
    
        #region BackgroundImages

        public static Texture2D space;
        public static Texture2D samurai;
        public static Texture2D blueBackground;
        public static Texture2D ff15Background;

        #endregion
        #region MenuComponents

        public static Texture2D redButtonNoHover;
        public static Texture2D redButtonHover;
        public static Texture2D btnNewGame;
        public static Texture2D glowingButton;
        public static Texture2D discoButton;
        public static Texture2D heart;
        public static Texture2D xboxButtons_A;

        #endregion
        #region Characters

        public static Texture2D warrior;
        public static Texture2D bowlingBall;
        public static Texture2D swordsman;

        #endregion
        #region Other

        public static Texture2D rectangleTex;

        #endregion

        #endregion
        #region SpriteFonts

        public static SpriteFont arial12;
        public static SpriteFont arial15;
        public static SpriteFont arial18;
        public static SpriteFont arial20;
        public static SpriteFont arial30;
        public static SpriteFont arial35;

        public static SpriteFont vecna22;
        public static SpriteFont vecnaBold22;

        public static SpriteFont manaSpace22;
        public static SpriteFont manaSpace25;

        #endregion
        #region Methods
        /// <summary>
        /// Uses given ContentManager and GraphicsDevice to load all Content declared inside Contents class.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadAll(ContentManager c, GraphicsDevice g)
        {
            // space = c.Load<Texture2D>("");
            // samurai = c.Load<Texture2D>("");
            blueBackground = c.Load<Texture2D>("Backgrounds/blueBackground");
            ff15Background = c.Load<Texture2D>("Backgrounds/ff15Background");

            redButtonNoHover = c.Load<Texture2D>("MenuComponents/RedButtonNoHover");
            redButtonHover = c.Load<Texture2D>("MenuComponents/RedButtonHover");
            btnNewGame = c.Load<Texture2D>("MenuComponents/NewGameButton");
            glowingButton = c.Load<Texture2D>("MenuComponents/GlowingButton");
            discoButton = c.Load<Texture2D>("MenuComponents/DiscoButton");
            heart = c.Load<Texture2D>("MenuComponents/Heart");
            xboxButtons_A = c.Load<Texture2D>("MenuComponents/XboxButtons_A");

            warrior = c.Load<Texture2D>("Characters/warrior");
            bowlingBall = c.Load<Texture2D>("Characters/BowlingBall");
            swordsman = c.Load<Texture2D>("Characters/Swordsman");

            rectangleTex = new Texture2D(g, 1, 1, false, SurfaceFormat.Color);
            rectangleTex.SetData(new[] { Color.White });

            arial12 = c.Load<SpriteFont>("SpriteFonts/Arial12");
            arial15 = c.Load<SpriteFont>("SpriteFonts/Arial15");
            arial18 = c.Load<SpriteFont>("SpriteFonts/Arial18");
            arial20 = c.Load<SpriteFont>("SpriteFonts/Arial20");
            arial30 = c.Load<SpriteFont>("SpriteFonts/Arial30");
            arial35 = c.Load<SpriteFont>("SpriteFonts/Arial35");

            vecna22 = c.Load<SpriteFont>("SpriteFonts/Vecna22");
            vecnaBold22 = c.Load<SpriteFont>("SpriteFonts/VecnaBold22");

            manaSpace22 = c.Load<SpriteFont>("SpriteFonts/ManaSpace22");
            manaSpace25 = c.Load<SpriteFont>("SpriteFonts/ManaSpace25");
        }

        /// <summary>
        /// Uses given ContentManager to load all Character related Textures.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadCharacters(ContentManager c)
        {
            warrior = c.Load<Texture2D>("Characters/warrior");
            bowlingBall = c.Load<Texture2D>("Characters/BowlingBall");
            swordsman = c.Load<Texture2D>("Characters/Swordsman");
        }
        #endregion
    }
}