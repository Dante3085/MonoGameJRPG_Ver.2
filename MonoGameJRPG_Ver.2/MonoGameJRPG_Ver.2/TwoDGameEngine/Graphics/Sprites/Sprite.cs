using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;
using IDrawable = MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.IDrawable;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public class Sprite : GameObject, ICollidable, IEntity, IInputable, GameLogic.States.IEntity, IDrawable
    {
        public static bool drawBoundingBox = true;

        #region MemberVariables

        /// <summary>
        /// string identifier of the Sprite (Usefull in collisions etc.)
        /// </summary>
        protected string _name;

        /// <summary>
        /// Remembers if this Sprite is playerControlled or not.
        /// </summary>
        protected bool _isPlayerControlled;

        /// <summary>
        /// Remembers if this Sprite can be interacted with.
        /// </summary>
        protected bool _isInteractable;

        /// <summary>
        /// Remembers if this Sprite's interactionPrompt should be drawn.
        /// </summary>
        protected bool _drawInteractionPrompt;

        /// <summary>
        /// Stores this Sprite's spritesheet.
        /// </summary>
        protected Texture2D Spritesheet;

        /// <summary>
        /// Stores this Sprite's Position in the 2D-World.
        /// </summary>
        public Vector2 _position;

        /// <summary>
        ///  Stores this Sprite's velocity.
        /// </summary>
        protected Vector2 _velocity;

        /// <summary>
        /// Constant that will be applied to velocity for movement.
        /// </summary>
        protected int _speed = 250;

        /// <summary>
        /// KeyboardInput for controlling this Sprite with a Keyboard.
        /// </summary>
        protected KeyboardInput _keyboardInput;

        /// <summary>
        /// GamePadInput for controlling this Sprite with a GamePad.
        /// </summary>
        protected GamePadInput _gamePadInput;

        /// <summary>
        /// Describes collision behaviour. Should be called in "HandleCollision()" Method.
        /// </summary>
        protected Action<ICollidable> _collisionHandler;

        #region BoundingBox

        /// <summary>
        /// Rectangle specifying a Box that can be drawn around the Sprite using Util.DrawRectangleOutline()
        /// </summary>
        protected Rectangle _boundingBox;

        /// <summary>
        /// Rectangle array with 4 Rectangle objects, to be used for drawing the BoundingBox with Util.DrawRectangleOutline()
        /// </summary>
        protected Rectangle[] _boundingBoxLines = { new Rectangle(), new Rectangle(), new Rectangle(), new Rectangle() };

        #endregion

        /// <summary>
        /// PlayerIndex (relevant for GamePad etc.)
        /// </summary>
        protected PlayerIndex _playerIndex;

        #endregion
        #region Properties

        /// <summary>
        /// Name of this Sprite.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Rectangle specifying a Box that can be drawn around the Sprite using Util.DrawRectangleOutline()
        /// </summary>
        public Rectangle BoundingBox => _boundingBox;

        /// <summary>
        /// PlayerIndex of this Sprite.
        /// </summary>
        public PlayerIndex PlayerIndex => _playerIndex;

        /// <summary>
        /// Remembers if this Sprite is player controlled.
        /// </summary>
        public bool IsPlayerControlled
        {
            get => _isPlayerControlled;
            set => _isPlayerControlled = value;
        }

        protected bool _collisionDetected = false;

        /// <summary>
        /// Remembers if this Sprite can be interacted with.
        /// </summary>
        public bool IsInteractable
        {
            get => _isInteractable;
            set => _isInteractable = value;
        }

        /// <summary>
        /// Gets or sets whether InteractionPrompt of this Sprite should be drawn.
        /// Has same name as private method => prefix: Prop_
        /// </summary>
        public bool Prop_DrawInteractionPrompt
        {
            get => _drawInteractionPrompt;
            set => _drawInteractionPrompt = value;
        }

        /// <summary>
        /// This Sprite's KeyboardInput.
        /// </summary>
        public KeyboardInput KeyboardInput
        {
            get => _keyboardInput;
            set => _keyboardInput = value;
        }

        /// <summary>
        /// This Sprite's GamePadInput.
        /// </summary>
        public GamePadInput GamePadInput
        {
            get => _gamePadInput;
            set => _gamePadInput = value;
        }

        public Action<ICollidable> CollisionHandler
        {
            get => _collisionHandler;
            set => _collisionHandler = value;
        }

        #endregion

        /// <summary>
        /// Enum for specifying which side the interactionPrompt should be drawn at.
        /// </summary>
        protected enum Side
        {
            Left,
            Top,
            Right,
            Bottom
        }

        /// <summary>
        /// Constructs a Sprite meant for a player to controll it => It can respond to input and has a PlayerIndex.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spritesheet"></param>
        /// <param name="position"></param>
        /// <param name="keyboardInput"></param>
        /// <param name="gamePadInput"></param>
        /// <param name="playerIndex"></param>
        /// <param name="isInteractable"></param>
        public Sprite(string name, Texture2D spritesheet, Vector2 position, KeyboardInput keyboardInput = null,
            GamePadInput gamePadInput = null, PlayerIndex playerIndex = PlayerIndex.One, bool isInteractable = false,
            Action<ICollidable> collisionHandler = null)
        {
            _isPlayerControlled = true;

            _name = name;
            Spritesheet = spritesheet;
            _position = position;
            _keyboardInput = keyboardInput;
            _gamePadInput = gamePadInput;
            _playerIndex = playerIndex;
            _isInteractable = isInteractable;
            _collisionHandler = collisionHandler;

            _boundingBox = new Rectangle((int)position.X, (int)position.Y, spritesheet.Width, spritesheet.Height);

            if (_collisionHandler == null)
            {
                _collisionHandler = partner =>
                {
                    Game1.gameConsole.Log("Collision[" + Name + "|" + partner.Name + "]: No behaviour specified!");
                    _drawInteractionPrompt = true;
                };
            }
        }

        /// <summary>
        /// Constructs a Sprite not meant for a player to controll it => Controlled by AI or not movable at all.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spritesheet"></param>
        /// <param name="position"></param>
        /// <param name="isInteractable"></param>
        public Sprite(string name, Texture2D spritesheet, Vector2 position, bool isInteractable = false,
            Action<ICollidable> collisionHandler = null)
        {
            _isPlayerControlled = false;

            _name = name;
            Spritesheet = spritesheet;
            _position = position;
            _isInteractable = isInteractable;
            _collisionHandler = collisionHandler;

            _boundingBox = new Rectangle((int)position.X, (int)position.Y, spritesheet.Width, spritesheet.Height);

            if (_collisionHandler == null)
            {
                _collisionHandler = partner =>
                {
                    Game1.gameConsole.Log("Collision[" + Name + "|" + partner.Name + "]: No behaviour specified!");
                    _drawInteractionPrompt = true;
                };
            }
        }

        /// <summary>
        /// Updates Sprite.
        /// Handle input, update position, update bounding box position
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            // Only handle input if Sprite is playerControlled.
            if (_isPlayerControlled)
            {
                // If GamePad is connected, handle it's input. Else, handle Keyboard's input.
                if (InputManager.GamePadConnected())
                    HandleGamePadInput(gameTime);
                else
                    HandleKeyboardInput(gameTime);
            }

            // Apply Velocity to Position.
            _position.X += (float)((double)_velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
            _position.Y += (float)((double)_velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);

            // Update BoundingBox.
            _boundingBox.X = (int)_position.X;
            _boundingBox.Y = (int)_position.Y;

            // Reset Velocity. Prevents Sprite from moving without there being actual input.
            _velocity = Vector2.Zero;

            _drawInteractionPrompt = false;
        }

        /// <summary>
        /// Draw this Sprite with passed SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Spritesheet, _position, Color.White);

            if (_drawInteractionPrompt)
                DrawInteractionPrompt(spriteBatch, Side.Top);

            if (drawBoundingBox)
            {
                if (_collisionDetected)
                    Util.DrawRectangleOutline(_boundingBox, _boundingBoxLines, Contents.rectangleTex, Color.Red, spriteBatch);
                else
                    Util.DrawRectangleOutline(_boundingBox, _boundingBoxLines, Contents.rectangleTex, Color.Blue, spriteBatch);
            }

            // Reset flag for collision detection.
            _collisionDetected = false;
        }

        /// <summary>
        /// Draws this Sprite's InteractioPrompt on the specified side.
        /// </summary>
        protected void DrawInteractionPrompt(SpriteBatch spriteBatch, Side side)
        {
            Vector2 position = _position;
            switch (side)
            {
                case Side.Left:
                {
                    //spriteBatch.Draw(Contents.xboxButtons_A, );
                    break;
                }

                case Side.Top:
                {
                    position.Y -= Contents.xboxButtons_A.Height;
                    position.X += Contents.xboxButtons_A.Width - 2;
                    spriteBatch.Draw(Contents.xboxButtons_A, position, Color.White);
                    break;
                }

                case Side.Right:
                {
                    break;
                }

                case Side.Bottom:
                {
                    break;
                }
            }
        }

        #region Movement
        protected virtual void GoLeft()
        {
            _velocity.X = -_speed;
        }

        protected virtual void GoUp()
        {
            _velocity.Y = -_speed;
        }

        protected virtual void GoRight()
        {
            _velocity.X = _speed;
        }

        protected virtual void GoDown()
        {
            _velocity.Y = _speed;
        }
        #endregion
        #region HandleInput

        /// <summary>
        /// Handles basic KeyboardInput for this Sprite.
        /// </summary>
        public virtual void HandleKeyboardInput(GameTime gameTime)
        {
            // LEFT
            if (InputManager.IsKeyDown(_keyboardInput.Left))
                GoLeft();

            // UP
            if (InputManager.IsKeyDown(_keyboardInput.Up))
                GoUp();

            // RIGHT
            if (InputManager.IsKeyDown(_keyboardInput.Right))
                GoRight();

            // DOWN
            if (InputManager.IsKeyDown(_keyboardInput.Down))
                GoDown();

            // SPRINT
            if (InputManager.OnKeyDown(_keyboardInput.Run))
                _speed += 100;
            else if (InputManager.OnKeyUp(_keyboardInput.Run))
                _speed -= 100;
        }


        /// <summary>
        /// Handles basic GamePadInput for this Sprite.
        /// </summary>
        public virtual void HandleGamePadInput(GameTime gameTime)
        {
            // LEFT
            if (InputManager.IsButtonDown(_gamePadInput.Left))
                GoLeft();

            // UP
            if (InputManager.IsButtonDown(_gamePadInput.Up))
                GoUp();

            // RIGHT
            if (InputManager.IsButtonDown(_gamePadInput.Right))
                GoRight();

            // DOWN
            if (InputManager.IsButtonDown(_gamePadInput.Down))
                GoDown();

            // SPRINT
            if (InputManager.OnButtonDown(_gamePadInput.Run))
            {
                _speed += 100;
            }
            else if (InputManager.OnButtonUp(_gamePadInput.Run))
                _speed -= 100;
        }

        #endregion
        #region Collision

        /// <summary>
        /// Checks whether or not this Sprite collides with partner.
        /// For this check each Sprite's BoundingBoxes are used.
        /// Returns true if both BoundingBoxes intersected.
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public virtual bool CollidesWith(ICollidable partner)
        {
            // true: Es wird nicht mit sich selbst verglichen und die BoundingBoxen überschneiden sich. false: sonst.
            return !this.Equals(partner) && this._boundingBox.Intersects(partner.BoundingBox);
        }

        /// <summary>
        /// Called in case of collision. Handles collision.
        /// </summary>
        /// <param name="partner"></param>
        public void HandleCollision(ICollidable partner)
        {
            _collisionDetected = true;
            _collisionHandler(partner);
        }

        #endregion

        /// <summary>
        /// Compiles relevant information about this Sprite in a string and returns it.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name + "[" + _position.X + "|" + _position.Y + "]";
        }
    }
}
