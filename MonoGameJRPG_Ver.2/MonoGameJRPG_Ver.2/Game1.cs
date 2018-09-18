
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites.Combos;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;
using VosSoft.Xna.GameConsole;
using IEntity = MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States.IEntity;

namespace MonoGameJRPG_Ver._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// Singleton Instance of Game1 class.
        /// </summary>
        private static Game1 _gameInstance;

        #region MemberVariables

        public static GameConsole gameConsole;
        public static float fps;
        public static int screenWidth;
        public static int screenHeight;

        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Text fpsText;
        private Text timeText;
        private Time time;
        private VBox timeFpsBox;

        private FiniteStateMachine finiteStateMachine;

        #region Test

        #endregion
        #endregion

        #region Properties

        /// <summary>
        /// Returns Singleton Instance of Game1 class.
        /// Instantiates that instance if it hasn't been called yet.
        /// </summary>
        // public static Game1 Game => _gameInstance ?? (_gameInstance = new Game1());

        public static Game1 Game
        {
            get
            {
                if (_gameInstance == null)
                    _gameInstance = new Game1();
                return _gameInstance;
            }
        }

        public FiniteStateMachine FiniteStateMachine => finiteStateMachine;

        #endregion

        private Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            //screenWidth = 600;
            //screenHeight = 300;

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

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

            Contents.LoadAll(Content, GraphicsDevice);
            #region InstantiateGraphicalContent

            // Fps
            fpsText = new Text("fps", 0, 0, "" + fps, () => { gameConsole.Log("Clicked me"); });

            time = new Time();
            timeText = new Text(time.ToString());

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            Character adventurer = new Character("adventurer", isPlayerControlled: true,
                keyboardInput: KeyboardInput.Default(), 
                animatedSprite: SpriteFactory.Adventurer(Vector2.Zero));

            Character swordsman = new Character("swordsman", isPlayerControlled: true,
                keyboardInput: KeyboardInput.Alternative(),
                animatedSprite: SpriteFactory.Swordsman(Vector2.Zero));

            CollisionManager c = new CollisionManager(adventurer.AnimatedSprite,
                swordsman.AnimatedSprite);

            finiteStateMachine = new FiniteStateMachine(new Dictionary<EState, State>()
            {
                { EState.EmptyState, new EmptyState(new List<EState>(){EState.MainMenuState})},
                { EState.MainMenuState, StateFactory.DefaultMainMenuState() },
                { EState.TestLevelState, StateFactory.TestLevelState(adventurer.AnimatedSprite, 
                    swordsman.AnimatedSprite, c) },
                { EState.InventoryState, StateFactory.InventoryState(adventurer, swordsman) }
            });
            finiteStateMachine.Change(EState.MainMenuState);

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

            // Time
            timeText.SetText(time.ToString());
            time.Update(gameTime);

            // Console
            if (InputManager.OnKeyDown(Keys.Tab))
                gameConsole.Open(Keys.Tab);

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            // Exit Game (Debugging)
            if (InputManager.IsKeyDown(Keys.Escape) || InputManager.IsButtonDown(Buttons.Back))
                Exit();

            finiteStateMachine.Update(gameTime);

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

            spriteBatch.Begin();
            #region Drawing

            // (Content of this region is only meant for debugging purposes.)
            #region Test

            finiteStateMachine.Draw(spriteBatch);

            #endregion

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
