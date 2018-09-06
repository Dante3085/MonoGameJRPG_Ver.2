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

        public static Texture2D blueBackground;
        public static Texture2D ff15Background;
        public static Texture2D blackBackground;

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

        public static SpriteFont manaSpace18;
        public static SpriteFont manaSpace19;
        public static SpriteFont manaSpace20;
        public static SpriteFont manaSpace21;
        public static SpriteFont manaSpace22;
        public static SpriteFont manaSpace23;
        public static SpriteFont manaSpace24;
        public static SpriteFont manaSpace25;
        public static SpriteFont manaSpace26;
        public static SpriteFont manaSpace27;
        public static SpriteFont manaSpace28;

        #endregion
        #region Methods
        /// <summary>
        /// Uses given ContentManager and GraphicsDevice to load all Content declared inside Contents class.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadAll(ContentManager c, GraphicsDevice g)
        {
            blueBackground = c.Load<Texture2D>("Backgrounds/blueBackground");
            ff15Background = c.Load<Texture2D>("Backgrounds/ff15Background");
            blackBackground = c.Load<Texture2D>("Backgrounds/blackBackground");

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

            manaSpace18 = c.Load<SpriteFont>("SpriteFonts/ManaSpace18");
            manaSpace19 = c.Load<SpriteFont>("SpriteFonts/ManaSpace19");
            manaSpace20 = c.Load<SpriteFont>("SpriteFonts/ManaSpace20");
            manaSpace21 = c.Load<SpriteFont>("SpriteFonts/ManaSpace21");
            manaSpace22 = c.Load<SpriteFont>("SpriteFonts/ManaSpace22");
            manaSpace23 = c.Load<SpriteFont>("SpriteFonts/ManaSpace23");
            manaSpace24 = c.Load<SpriteFont>("SpriteFonts/ManaSpace24");
            manaSpace25 = c.Load<SpriteFont>("SpriteFonts/ManaSpace25");
            manaSpace26 = c.Load<SpriteFont>("SpriteFonts/ManaSpace26");
            manaSpace27 = c.Load<SpriteFont>("SpriteFonts/ManaSpace27");
            manaSpace28 = c.Load<SpriteFont>("SpriteFonts/ManaSpace28");
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