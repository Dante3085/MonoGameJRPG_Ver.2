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

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 300;
            graphics.PreferredBackBufferHeight = 200;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
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
            bowlingBall = new AnimatedSprite("BowlingBall", new Vector2(0, 0), PlayerIndex.One, Contents.bowlingBall, new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S
            });
            bowlingBall.AddAnimation(9, 32, 0, "Left", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(9, 0, 0, "Up", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(9, 0, 0, "Right", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(9, 0, 0, "Down", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(1, 0, 0, "IdleLeft", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(1, 0, 0, "IdleUp", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(1, 0, 0, "IdleRight", 32, 32, Vector2.Zero);
            bowlingBall.AddAnimation(1, 0, 0, "IdleDown", 32, 32, Vector2.Zero);

            newGameBtn = new AnimatedSprite("NewGameBtn", new Vector2(100, 100), PlayerIndex.One, Contents.btnNewGame, new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S
            });
            newGameBtn.AddAnimation(8, 0, 0, "IdleDown", 32, 32, Vector2.Zero);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
