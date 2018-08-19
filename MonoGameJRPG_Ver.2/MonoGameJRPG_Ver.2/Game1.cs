using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;
using VosSoft.Xna.GameConsole;

namespace MonoGameJRPG_Ver._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static GameConsole gameConsole;

        public static float fps;
        private Text fpsText;

        #region Test

        //private AnimatedMenuButton animBtn;
        //private AnimatedMenuButton animBtn2;

        private VBox vbox;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

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

            Contents.LoadAll(Content, GraphicsDevice);
            #region InstantiateGraphicalContent

            // Fps
            fpsText = new Text(Contents.arial12, Contents.arial15, "" + fps, 0, 0, () => { gameConsole.Log("Clicked me"); });

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            //animBtn = MenuFactory.AnimButton(new Vector2(100, 100));
            //animBtn2 = MenuFactory.AnimButton(new Vector2(200, 100));

            vbox = new VBox(0, 0, 10, null, new MenuElement[]
            {
                fpsText,
                MenuFactory.AnimButton(Vector2.Zero),
                MenuFactory.AnimButton(Vector2.Zero),
                MenuFactory.AnimButton(Vector2.Zero),
                MenuFactory.AnimButton(Vector2.Zero),
                MenuFactory.AnimButton(Vector2.Zero),
                MenuFactory.AnimButton(Vector2.Zero),
            });

            #endregion

            #endregion

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
            InputManager.UpdateCurrentStates();
            #region UpdateLogic

            // Fps
            fps = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            fpsText.SetText("Fps: " + fps);
            fpsText.Update(gameTime);

            // Console
            if (InputManager.OnKeyDown(Keys.Tab))
                gameConsole.Open(Keys.Tab);

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            // Exit Game (Debugging)
            if (InputManager.IsKeyDown(Keys.Escape) || InputManager.IsButtonDown(Buttons.Back))
                Exit();

            //animBtn.Update(gameTime);
            //animBtn2.Update(gameTime);

            vbox.Update(gameTime);

            #endregion

            #endregion
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

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            //animBtn.Render(spriteBatch);
            //animBtn2.Render(spriteBatch);

            vbox.Render(spriteBatch);

            #endregion

            fpsText.Render(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
