
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
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;
using VosSoft.Xna.GameConsole;

namespace MonoGameJRPG_Ver._2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
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

        public SceneStack _sceneStack;

        #region Test

        #endregion
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
        }

        public void ExitGame()
        {
            Exit();
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

            Character player = new Character("Player", maxHp: 1000, isPlayerControlled: true, keyboardInput: KeyboardInput.Default(), animatedSprite: SpriteFactory.Swordsman(Vector2.Zero));
            Character npc = new Character("Npc", isPlayerControlled: false, gamePadInput: GamePadInput.Default(),
                animatedSprite: SpriteFactory.NPC(new Vector2(100, 100)));

            Text inventoryText = new Text("", x: 10, y: 10, text: "Open/Close Inventory: 'START' (GamePad), 'I' (Keyboard)");
            inventoryText.SetColor(Color.Aquamarine);
            inventoryText.CursorOnIt = true;

            _sceneStack = new SceneStack(new Dictionary<EScene, Scene>()
            {
                { EScene.MainMenuScene, SceneFactory.MainMenuScene(this) },
                { EScene.FirstLevelScene, SceneFactory.FirstLevelScene(this, player, npc, inventoryText) },
                { EScene.InventoryScene, SceneFactory.InventoryScene(this, player, npc) }
            });
            _sceneStack.Push(EScene.MainMenuScene);

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

            _sceneStack.Update(gameTime);

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

            _sceneStack.Draw(spriteBatch);

            #endregion

            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
