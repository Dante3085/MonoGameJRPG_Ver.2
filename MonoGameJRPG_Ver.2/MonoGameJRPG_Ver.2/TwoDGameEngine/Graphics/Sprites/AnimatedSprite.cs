using System;
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
    /// <summary>
    /// An AnimatedSprite is a Sprite that can make use of a Spritesheet to create Animation.
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        #region MemberVariables

        // Variables for handling the Animation.

        private Dictionary<EAnimation, Rectangle[]> _animations = new Dictionary<EAnimation, Rectangle[]>();
        private Dictionary<EAnimation, Vector2> _offsets = new Dictionary<EAnimation, Vector2>();

        private double _timeElapsed;
        private double _timeToUpdate;

        private int _frameIndex;
        private EAnimation _currentAnimation;

        private enum Direction { none, left, up, right, down };
        private Direction _currentDirection = Direction.none;

        private bool _isAttacking = false;
        private bool _playingAnimation = false;
        private bool _collisionDetected = false;

        #endregion

        #region Properties
        public int Fps { get => (int)_timeToUpdate; set => _timeToUpdate = 1f / value; }
        public bool PlayingAnimation { get => _playingAnimation; set => _playingAnimation = value; }
        #endregion

        /// <summary>
        /// Constructs an AnimatedSprite meant for a player to controll it => It can respond to input and has a PlayerIndex.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="playerIndex"></param>
        /// <param name="fps"></param>
        /// <param name="texture"></param>
        /// <param name="keyboardInput"></param>
        /// <param name="gamePadInput"></param>
        public AnimatedSprite(string name, Texture2D texture, Vector2 position, PlayerIndex playerIndex, int fps = 20, KeyboardInput keyboardInput = null, GamePadInput gamePadInput = null) : base(name, texture, position, keyboardInput, gamePadInput, playerIndex)
        {
            _isPlayerControlled = true;

            Fps = fps;
            PlayAnimation(EAnimation.Idle);
        }

        /// <summary>
        /// Constructs a Sprite not meant for a player to controll it => Controlled by AI or not movable at all.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="fps"></param>
        public AnimatedSprite(string name, Texture2D texture, Vector2 position, int fps = 20) : base(name, texture, position)
        {
            _isPlayerControlled = false;
            Fps = fps;
            PlayAnimation(EAnimation.Idle);
        }

        /// <summary>
        /// animation := Enum identifier for Animation. <para></para>
        /// numFrames := number of frames in Animation. <para></para>
        /// frameWidth := width of single frame in Animation. <para></para>
        /// frameHeight := height of single frame in Animation. <para></para>
        /// yRow := y-coordinate of row that contains frames for Animation. <para></para>
        /// indexFirstFrame := index (0 ... n) of first frame for Animation in said row. <para></para>
        /// offset := x-, y-coordinate offset for frames that have a different size.
        /// </summary>
        /// <param animation="numFrames"></param>
        /// <param animation="yRow"></param>
        /// <param animation="indexFirstFrame"></param>
        /// <param animation="name"></param>
        /// <param animation="frameWidth"></param>
        /// <param animation="frameHeight"></param>
        /// <param animation="offset"></param>
        public void AddAnimation(EAnimation name, int numFrames, int frameWidth, int frameHeight, int yRow, int indexFirstFrame, Vector2 offset)
        {
            // Creates an array of rectangles (i.e. a new Animation).
            Rectangle[] animation = new Rectangle[numFrames];

            // Fills up the array of rectangles
            for (int i = 0; i < numFrames; i++)
                animation[i] = new Rectangle((i + indexFirstFrame) * frameWidth, yRow, frameWidth, frameHeight);

            // Store frames and offset in two different dictionaries. But both with same key (animation.)
            _animations.Add(name, animation);
            _offsets.Add(name, offset);
        }

        /// <summary>
        /// Central Methods for HandleKeyboardInput and HandleGamePadInput.
        /// This way, both methods don't have to have their own treatment for the same input actions.
        /// If something is changed here, both Keyboard and GamePad can immediately execute that change.
        /// </summary>
        #region InputHelperMethods
        protected override void GoLeft()
        {
            base.GoLeft();
            PlayAnimation(EAnimation.Left);
            _currentDirection = Direction.left;
        }

        protected override void GoUp()
        {
            base.GoUp();
            PlayAnimation(EAnimation.Up);
            _currentDirection = Direction.up;
        }

        protected override void GoRight()
        {
            base.GoRight();
            PlayAnimation(EAnimation.Right);
            _currentDirection = Direction.right;
        }

        protected override void GoDown()
        {
            base.GoDown();
            PlayAnimation(EAnimation.Down);
            _currentDirection = Direction.down;
        }

        protected void Idle()
        {
            if (_currentAnimation == EAnimation.Left)
                PlayAnimation(EAnimation.IdleLeft);

            if (_currentAnimation == EAnimation.Up)
                PlayAnimation(EAnimation.IdleUp);

            if (_currentAnimation == EAnimation.Right)
                PlayAnimation(EAnimation.IdleRight);

            if (_currentAnimation == EAnimation.Down)
                PlayAnimation(EAnimation.IdleDown);
        }

        protected void Attack()
        {
            _isAttacking = true;
            // LEFT ATTACK
            if (_currentAnimation == EAnimation.Left)
            {
                PlayAnimation(EAnimation.MeleeLeft);
                _currentDirection = Direction.left;

                _boundingBox.Y -= 5;
                _boundingBox.X -= 20;
            }

            // UP ATTACK
            if (_currentAnimation == EAnimation.Up)
            {
                PlayAnimation(EAnimation.MeleeUp);
                _currentDirection = Direction.up;

                _boundingBox.Y -= 20;
                _boundingBox.X -= 5;
            }

            // RIGHT ATTACK
            if (_currentAnimation == EAnimation.Right)
            {
                PlayAnimation(EAnimation.MeleeRight);
                _currentDirection = Direction.right;

                _boundingBox.Y -= 5;
                _boundingBox.X += 5;
            }

            // DOWN ATTACK
            if (_currentAnimation == EAnimation.Down)
            {
                PlayAnimation(EAnimation.MeleeDown);
                _currentDirection = Direction.down;

                _boundingBox.X -= 5;
            }
        }

        #endregion 

        public override void HandleKeyboardInput()
        {
            // Directional Movement or Idle
            if (!_isAttacking)
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

                // No Movement => Idle Frame for respective Direction.
                else
                    Idle();
            }

            // ATTACKING
            if (InputManager.IsKeyDown(_keyboardInput.Attack))
                Attack();

            _currentDirection = Direction.none;
        }

        public override void HandleGamePadInput()
        {
            // Directional Movement or Idle
            if (!_isAttacking)
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

                // No Movement => Idle Frame for respective Direction.
                else
                    Idle();
            }

            // ATTACKING
            if (InputManager.IsButtonDown(_gamePadInput.Attack))
                Attack();

            _currentDirection = Direction.none;
        }

        /// <summary>
        /// Handles frame Update.
        /// </summary>
        /// <param animation="GameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            // HandleInput, Update Position, Update BoundingBox Position.
            base.Update(gameTime);

            if (IsPlayerControlled)
            {
                // If GamePad is connected, handle it's input. Else, handle Keyboard's input.
                // Da das Sprite eigentlich schon weiß, ob Input per Keyboard oder GamePad kommt,
                // könnte dies irgendwie an das AnimatedSprite weitergeleitet werden.
                if (GamePad.GetState(_playerIndex).IsConnected)
                    HandleGamePadInput();
                else
                    HandleKeyboardInput();
            }

            // Adds time that has elapsed since our last draw
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // We need to change our image if the timeElapsed is greater than our timeToUpdate(calculated by our framerate)
            if (_timeElapsed > _timeToUpdate)
            {
                // Resets the timer in a way, so that we keep our desired Fps
                _timeElapsed -= _timeToUpdate;

                // Increment frameIndex
                if (_frameIndex < _animations[_currentAnimation].Length - 1)
                    _frameIndex++;

                // Restarts the animation
                else
                {
                    AnimationDone(_currentAnimation);
                    _frameIndex = 0;
                }
            }

            // Update BoundingBox size to fit current frame size.
            _boundingBox.Width = _animations[_currentAnimation][_frameIndex].Width;
            _boundingBox.Height = _animations[_currentAnimation][_frameIndex].Height;
        }

        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param animation="spriteBatch">SpriteBatch</param>
        // TODO: Collide-Logic shouldn't be in here!
        public void Draw(SpriteBatch spriteBatch, List<AnimatedSprite> animSprites = null)
        {
            spriteBatch.Draw(_texture, _position + _offsets[_currentAnimation], _animations[_currentAnimation][_frameIndex], Color.White);

            // Set _collisionDetected to true if collision happened.
            // This construct just detects if any collision happened. Not how many etc.
            if (animSprites != null)
                foreach (AnimatedSprite a in animSprites)
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
        /// Plays the animation specified by it's animation.
        /// </summary>
        /// <param animation="animation">Animation to play</param>
        private void PlayAnimation(EAnimation animation)
        {
            // Makes sure we won't start a new annimation unless it differs from our current animation.
            if (_currentAnimation != animation && _currentDirection.Equals(Direction.none))
            {
                _playingAnimation = true;

                _currentAnimation = animation;
                _frameIndex = 0;
            }
        }

        /// <summary>
        /// Called everytime a frame finishes.
        /// </summary>
        /// <param animation="animation"></param>
        private void AnimationDone(EAnimation animation)
        {
            if (animation == EAnimation.MeleeLeft || animation == EAnimation.MeleeUp ||
                animation == EAnimation.MeleeRight || animation == EAnimation.MeleeDown)
                _isAttacking = false;

            _playingAnimation = false;
        }
    }
}
