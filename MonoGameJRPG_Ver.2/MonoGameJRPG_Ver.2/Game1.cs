using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;
using VosSoft.Xna.GameConsole;

namespace MonoGameJRPG_Ver._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static GameConsole gameConsole;

        public static float fps = -1;
        private Text fpsText;

        private Text numGameObjecText;

        #region Test

        private AnimatedSprite bowlingBall;
        private AnimatedSprite newGameBtn;
        private AnimatedSprite newGameBtn2;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameConsole = new GameConsole(this, "german", Content);
            gameConsole.IsFullscreen = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Contents.LoadAll(Content, GraphicsDevice);

            fpsText = new Text(Contents.arial12, Contents.arial15, "" + fps, 0, 0, () => { gameConsole.Log("Clicked me"); });
            bowlingBall = new AnimatedSprite("BowlingBall", Contents.bowlingBall, new Vector2(0, 0), PlayerIndex.One, 20, new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S
            });
            bowlingBall.AddAnimation(EAnimation.Idle, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleLeft, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleUp, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleRight, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleDown, 1, 32, 32, 0, 0, Vector2.Zero);

            bowlingBall.AddAnimation(EAnimation.Right, 9, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.Left, 9, 32, 32, 32, 0, Vector2.Zero);

            newGameBtn = new AnimatedSprite("NewGameBtn", Contents.btnNewGame, new Vector2(100, 100), 5);
            newGameBtn.AddAnimation(EAnimation.Idle, 8, 32, 32, 0, 0, Vector2.Zero);

            newGameBtn2 = new AnimatedSprite("NewGameBtn", Contents.btnNewGame, new Vector2(140, 100), 20);
            newGameBtn2.AddAnimation(EAnimation.Idle, 8, 32, 32, 0, 0, Vector2.Zero);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            InputManager.UpdateCurrentStates();

            fps = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            fpsText.SetText("Fps: " + fps);

            if (InputManager.OnKeyDown(Keys.Tab))
                gameConsole.Open(Keys.Tab);

            fpsText.Update(gameTime);
            bowlingBall.Update(gameTime);
            newGameBtn.Update(gameTime);
            newGameBtn2.Update(gameTime);

            InputManager.UpdatePreviousStates();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            fpsText.Render(spriteBatch);
            bowlingBall.Draw(spriteBatch);
            newGameBtn.Draw(spriteBatch);
            newGameBtn2.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
