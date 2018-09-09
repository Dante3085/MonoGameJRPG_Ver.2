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

        /// <summary>
        /// Stores Rectangle Arrays that each represent an Animation (1 Rectangle = 1 frame in an Animation, 1 Rectangle Array = 1 Animation).
        /// </summary>
        private Dictionary<EAnimation, Rectangle[]> _animations = new Dictionary<EAnimation, Rectangle[]>();

        /// <summary>
        /// Stores Vector2s used for offsetting certain Animations that may differ in size.
        /// </summary>
        private Dictionary<EAnimation, Vector2> _offsets = new Dictionary<EAnimation, Vector2>();

        /// <summary>
        /// Stores Fps values for each Animation. Due to varying number of frames between Animations, some may need
        /// to be played slower or faster.
        /// </summary>
        private Dictionary<EAnimation, int> _animationFpsValues = new Dictionary<EAnimation, int>();

        private Dictionary<EAnimation, Action> _onAnimationStartActions = new Dictionary<EAnimation, Action>();

        // private Dictionary<EAnimation, Action> _onAnimationFrameActions = new

        private Dictionary<EAnimation, Action> _onAnimationEndActions = new Dictionary<EAnimation, Action>();


        /// <summary>
        /// Accumulates time with each Update() call until _timeToUpdate is reached.
        /// </summary>
        private double _timeElapsed;

        /// <summary>
        /// Time threshold for switching to the next frame in the current Animation.
        /// </summary>
        private double _timeToUpdate;

        /// <summary>
        /// Index of currentFrame in currentAnimation (i.e. in current Rectangle[])
        /// </summary>
        private int _currentFrameIndex;

        /// <summary>
        /// Current Animation
        /// </summary>
        private EAnimation _currentAnimation = EAnimation.Idle;

        /// <summary>
        /// Enum to store in which direction the AnimatedSprite is facing.
        /// </summary>
        private enum Direction { none, left, up, right, down };

        /// <summary>
        /// Current direction in which the Sprite is faces.
        /// </summary>
        private Direction _currentDirection = Direction.none;

        private bool _playingAnimation = false;

        #endregion

        #region Properties
        public int Fps { get => (int)_timeToUpdate; set => _timeToUpdate = 1f / value; }
        public bool PlayingAnimation { get => _playingAnimation; set => _playingAnimation = value; }
        public int Width => _animations[_currentAnimation][_currentFrameIndex].Width;
        public int Height => _animations[_currentAnimation][_currentFrameIndex].Height;
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
        public AnimatedSprite(string name, Texture2D texture, Vector2 position, PlayerIndex playerIndex, int fps = 20, KeyboardInput keyboardInput = null, GamePadInput gamePadInput = null, bool isInteractable = false) 
            : base(name, texture, position, keyboardInput, gamePadInput, playerIndex, isInteractable)
        {
            _isPlayerControlled = true;
            Fps = fps;
        }

        /// <summary>
        /// Constructs a Sprite not meant for a player to controll it => Controlled by AI or not movable at all.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="fps"></param>
        public AnimatedSprite(string name, Texture2D texture, Vector2 position, int fps = 20, bool isInteractable = false) 
            : base(name, texture, position, isInteractable)
        {
            _isPlayerControlled = false;
            Fps = fps;
        }

        #region InputHelperMethods

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to the left.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoLeft()
        {
            base.GoLeft();
            PlayAnimation(EAnimation.Left);
            _currentDirection = Direction.left;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to up.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoUp()
        {
            base.GoUp();
            PlayAnimation(EAnimation.Up);
            _currentDirection = Direction.up;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to the right.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoRight()
        {
            base.GoRight();
            PlayAnimation(EAnimation.Right);
            _currentDirection = Direction.right;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to down.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoDown()
        {
            base.GoDown();
            PlayAnimation(EAnimation.Down);
            _currentDirection = Direction.down;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to be in idle Animation.
        /// Depending on the direction the AnimatedSprite is facing, a certain Idle Animation will be played.
        /// </summary>
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

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to execute an Attack.
        /// Depending on the direction the AnimatedSprite is facing, a certain Attack Animation will be played.
        /// the bounding box will also be expanded accordingly.
        /// </summary>
        protected void Attack()
        {
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
        #region HandleInput

        public override void HandleKeyboardInput()
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

            // No Movement => Idle Frame for respective Direction.
            else
                Idle();

            _currentDirection = Direction.none;
        }
        public override void HandleGamePadInput()
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
                _speed += 100;
            else if (InputManager.OnButtonUp(_gamePadInput.Run))
                _speed -= 100;

            // No Movement => Idle Frame for respective Direction.
            else
                Idle();

            _currentDirection = Direction.none;
        }

        #endregion

        /// <summary>
        /// Updates the AnimatedSprite.
        /// Handle input, update animation, update bounding box size
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsPlayerControlled)
            {
                if (InputManager.GamePadConnected())
                    HandleGamePadInput();
                else
                    HandleKeyboardInput();
            }

            UpdateAnimation(gameTime);
        }

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param animation="spriteBatch">SpriteBatch</param>
        // TODO: Collide-Logic shouldn't be in here!
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position + _offsets[_currentAnimation], _animations[_currentAnimation][_currentFrameIndex], Color.White);

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
        /// Increments the frames of an Animation according to the Frames per second.
        /// Also executes methods that handle occurences at certain points in the Animation (start, end, etc.)
        /// Also updates BoundingBox size.
        /// </summary>
        /// <param name="gameTime"></param>
        private void UpdateAnimation(GameTime gameTime)
        {
            // Adds time that has elapsed since the last Update
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // We need to change our image if the timeElapsed is greater than our timeToUpdate(calculated by our framerate)
            if (_timeElapsed > _timeToUpdate)
            {
                // Resets the timer so that the desired Fps are achieved.
                _timeElapsed -= _timeToUpdate;

                // Increment frameIndex
                if (_currentFrameIndex < _animations[_currentAnimation].Length - 1)
                    _currentFrameIndex++;

                // Restarts the animation
                else
                {
                    OnAnimationEnd(_currentAnimation);
                    _currentFrameIndex = 0;
                }
            }

            // Update BoundingBox size to fit current frame size.
            _boundingBox.Width = _animations[_currentAnimation][_currentFrameIndex].Width;
            _boundingBox.Height = _animations[_currentAnimation][_currentFrameIndex].Height;
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
        public void AddAnimation(EAnimation name, int numFrames, int frameWidth, int frameHeight, int yRow, int indexFirstFrame, Vector2 offset, int fps)
        {
            // Creates an array of rectangles (i.e. a new Animation).
            Rectangle[] animation = new Rectangle[numFrames];

            // Fills up the array of rectangles
            for (int i = 0; i < numFrames; i++)
                animation[i] = new Rectangle((i + indexFirstFrame) * frameWidth, yRow, frameWidth, frameHeight);

            // Store frames and offset in two different dictionaries. But both with same key (animation.)
            _animations.Add(name, animation);
            _offsets.Add(name, offset);
            _animationFpsValues.Add(name, fps);
        }

        /// <summary>
        /// Plays the given animation.
        /// </summary>
        /// <param animation="animation">Animation to play</param>
        public void PlayAnimation(EAnimation animation)
        {
            // Makes sure we won't start a new annimation unless it differs from our current animation.
            if (_currentAnimation != animation && _currentDirection.Equals(Direction.none))
            {
                _playingAnimation = true;

                _currentAnimation = animation;
                _currentFrameIndex = 0;
                Fps = _animationFpsValues[animation];
            }
        }

        /// <summary>
        /// Called everytime a frame finishes.
        /// </summary>
        /// <param animation="animation"></param>
        private void OnAnimationEnd(EAnimation animation)
        {
            _playingAnimation = false;
        }

        private void OnAnimationStart(EAnimation animation)
        {

        }

        private void OnAnimationFrame(EAnimation animation, int frameIndex)
        {

        }
    }
}
