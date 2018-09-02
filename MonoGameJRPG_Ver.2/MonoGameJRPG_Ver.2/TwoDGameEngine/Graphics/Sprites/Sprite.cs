﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public class Sprite : GameObject, ICollidable
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
        /// Stores this Sprite's texture.
        /// </summary>
        protected Texture2D _texture;

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

        #region BoundingBox

        /// <summary>
        /// Rectangle specifying a Box that can be drawn around the Sprite using Util.DrawRectangle()
        /// </summary>
        protected Rectangle _boundingBox;

        /// <summary>
        /// Rectangle array with 4 Rectangle objects, to be used for drawing the BoundingBox with Util.DrawRectangle()
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
        /// Rectangle specifying a Box that can be drawn around the Sprite using Util.DrawRectangle()
        /// </summary>
        public Rectangle BoundingBox => _boundingBox;

        /// <summary>
        /// PlayerIndex of this Sprite.
        /// </summary>
        public PlayerIndex PlayerIndex => _playerIndex;

        /// <summary>
        /// Remembers if this Sprite is player controlled.
        /// </summary>
        public bool IsPlayerControlled => _isPlayerControlled;

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
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="keyboardInput"></param>
        /// <param name="gamePadInput"></param>
        /// <param name="playerIndex"></param>
        /// <param name="isInteractable"></param>
        public Sprite(string name, Texture2D texture, Vector2 position, KeyboardInput keyboardInput = null,
            GamePadInput gamePadInput = null, PlayerIndex playerIndex = PlayerIndex.One, bool isInteractable = false)
        {
            _isPlayerControlled = true;

            _name = name;
            _texture = texture;
            _position = position;
            _keyboardInput = keyboardInput;
            _gamePadInput = gamePadInput;
            _playerIndex = playerIndex;
            _isInteractable = isInteractable;

            _boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            // HandleConstructorDefaults();
        }

        /// <summary>
        /// Constructs a Sprite not meant for a player to controll it => Controlled by AI or not movable at all.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="isInteractable"></param>
        public Sprite(string name, Texture2D texture, Vector2 position, bool isInteractable = false)
        {
            _isPlayerControlled = false;

            _name = name;
            _texture = texture;
            _position = position;
            _isInteractable = isInteractable;

            _boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            // HandleConstructorDefaults();
        }

        ///// <summary>
        ///// Performs various assignments for default values passed in the constructors.
        ///// </summary>
        //private void HandleConstructorDefaults()
        //{
        //    // No Input specification passed => Default KeyboardInput
        //    if (_keyboardInput == null && _gamePadInput == null)
        //    {
        //        _keyboardInput = new KeyboardInput()
        //        {
        //            Left = Keys.A,
        //            Up = Keys.W,
        //            Right = Keys.D,
        //            Down = Keys.S
        //        };
        //    }
        //}

        /// <summary>
        /// Draw this Sprite with passed SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch, List<Sprite> sprites = null)
        {
            spriteBatch.Draw(_texture, _position, Color.White);

            if (_drawInteractionPrompt)
                DrawInteractionPrompt(spriteBatch, Side.Top);

            if (sprites != null)
                foreach (Sprite a in sprites)
                    if (this.CollidesWith(a))
                        _collisionDetected = true;

            if (drawBoundingBox)
            {
                if (_collisionDetected)
                    Util.DrawRectangle(spriteBatch, _boundingBox, _boundingBoxLines, Contents.rectangleTex, Color.Red);
                else
                    Util.DrawRectangle(spriteBatch, _boundingBox, _boundingBoxLines, Contents.rectangleTex, Color.Blue);
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

        public virtual void Update(GameTime gameTime)
        {
            // Only handle input if Sprite is playerControlled.
            if (_isPlayerControlled)
            {
                // If GamePad is connected, handle it's input. Else, handle Keyboard's input.
                if (GamePad.GetState(_playerIndex).IsConnected)
                    HandleGamePadInput();
                else
                    HandleKeyboardInput();
            }

            // Apply Velocity to Position.
            _position.X += (float)((double)_velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
            _position.Y += (float)((double)_velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);

            // Update BoundingBox.
            _boundingBox.X = (int)_position.X;
            _boundingBox.Y = (int)_position.Y;

            // Reset Velocity. Prevents Sprite from moving without there being actual input.
            _velocity = Vector2.Zero;
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
        public virtual void HandleKeyboardInput()
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
        }


        /// <summary>
        /// Handles basic GamePadInput for this Sprite.
        /// </summary>
        public virtual void HandleGamePadInput()
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
            // This can't collide with itself => Return false.
            if (this.Equals(partner))
            {
                Game1.gameConsole.Log(this.Name + " is equal to " + partner.Name);
                return false;
            }

            // Return whether or not this collides with partner.
            return this._boundingBox.Intersects(partner.BoundingBox);
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
