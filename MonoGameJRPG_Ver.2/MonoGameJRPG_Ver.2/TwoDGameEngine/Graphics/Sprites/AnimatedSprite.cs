using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites.Combos;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Input;
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
        /// Stores Fps values for each Animation.
        /// </summary>
        private Dictionary<EAnimation, int> _animationFpsValues = new Dictionary<EAnimation, int>();

        /// <summary>
        /// Stores Action methods supposed to be executed at the start of the respective Animation.
        /// </summary>
        private Dictionary<EAnimation, Action> _onAnimationStartActions = new Dictionary<EAnimation, Action>();

        // private Dictionary<EAnimation, Action> _onAnimationFrameActions = new

        /// <summary>
        /// Stores Action methods supposed to be executed at the end of the respective Animation.
        /// </summary>
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

        private Combo _combo;

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
        /// <param name="spritesheet"></param>
        /// <param name="keyboardInput"></param>
        /// <param name="gamePadInput"></param>
        public AnimatedSprite(string name, Texture2D spritesheet, Vector2 position = default(Vector2), 
            PlayerIndex playerIndex = PlayerIndex.One, int fps = 20, KeyboardInput keyboardInput = null, 
            GamePadInput gamePadInput = null, bool isInteractable = false) 
            : base(name, spritesheet, position, keyboardInput, gamePadInput, playerIndex, isInteractable)
        {
            _isPlayerControlled = true;
            Fps = fps;

            _combo = new Combo(new ComboNode(EAnimation.Melee1, new Dictionary<Keys, ComboNode>()
            {
                { Keys.F, new ComboNode(EAnimation.Melee2, new Dictionary<Keys, ComboNode>()
                {
                    { Keys.F, new ComboNode(EAnimation.Melee3, new Dictionary<Keys, ComboNode>(), new ComboNode.Intervall(0, 500), Keys.F, Buttons.X) }
                }, new ComboNode.Intervall(0, 500), Keys.F, Buttons.X) }
            }, new ComboNode.Intervall(0, 500), Keys.F, Buttons.X), this);
        }

        /// <summary>
        /// Constructs a Sprite not meant for a player to controll it => Controlled by AI or not movable at all.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="spritesheet"></param>
        /// <param name="fps"></param>
        public AnimatedSprite(string name, Texture2D spritesheet, int frameWidth, int frameHeight, Vector2 position = default(Vector2), 
            int fps = 20, bool isInteractable = false) 
            : base(name, spritesheet, position, isInteractable)
        {
            _isPlayerControlled = false;
            Fps = fps;

            _combo = new Combo(new ComboNode(EAnimation.Melee1, new Dictionary<Keys, ComboNode>()
            {
                { Keys.F, new ComboNode(EAnimation.Melee2, new Dictionary<Keys, ComboNode>()
                {
                    { Keys.F, new ComboNode(EAnimation.Melee3, new Dictionary<Keys, ComboNode>(), new ComboNode.Intervall(0, 2000), Keys.F, Buttons.X) }
                }, new ComboNode.Intervall(0, 2000), Keys.F, Buttons.X) }
            }, new ComboNode.Intervall(0, 2000), Keys.F, Buttons.X), this);
        }

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
                    HandleGamePadInput(gameTime);
                else
                    HandleKeyboardInput(gameTime);
            }

            _combo.Update(gameTime);
            PlayAnimation(gameTime);
        }

        #region InputHelperMethods

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to the left.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoLeft()
        {
            base.GoLeft();
            SetAnimation(EAnimation.Left);
            _currentDirection = Direction.left;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to up.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoUp()
        {
            base.GoUp();
            SetAnimation(EAnimation.Up);
            _currentDirection = Direction.up;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to the right.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoRight()
        {
            base.GoRight();
            SetAnimation(EAnimation.Right);
            _currentDirection = Direction.right;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to move to down.
        /// Change velocity, play animation.
        /// </summary>
        protected override void GoDown()
        {
            base.GoDown();
            SetAnimation(EAnimation.Down);
            _currentDirection = Direction.down;
        }

        /// <summary>
        /// Does everything that needs to happen for the AnimatedSprite to be in idle Animation.
        /// Depending on the direction the AnimatedSprite is facing, a certain Idle Animation will be played.
        /// </summary>
        protected void Idle()
        {
            if (_currentAnimation == EAnimation.Left)
                SetAnimation(EAnimation.IdleLeft);

            if (_currentAnimation == EAnimation.Up)
                SetAnimation(EAnimation.IdleUp);

            if (_currentAnimation == EAnimation.Right)
                SetAnimation(EAnimation.IdleRight);

            if (_currentAnimation == EAnimation.Down)
                SetAnimation(EAnimation.IdleDown);
        }

        #endregion
        #region HandleInput

        public override void HandleKeyboardInput(GameTime gameTime)
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

            //// Combo
            //if (InputManager.IsKeyDown(_keyboardInput.Combo))
            //    _combo.Update(gameTime);

            // No Movement => Idle Animation for respective Direction.
            else
                Idle();

            _currentDirection = Direction.none;
        }

        public override void HandleGamePadInput(GameTime gameTime)
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

            // Combo
            if (InputManager.IsButtonDown(_gamePadInput.Combo))
                _combo.Update(gameTime);

            // No Movement => Idle Frame for respective Direction.
            else
                Idle();

            _currentDirection = Direction.none;
        }

        #endregion

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param animation="spriteBatch">SpriteBatch</param>
        // TODO: Collide-Logic shouldn't be in here!
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Spritesheet, _position + _offsets[_currentAnimation], _animations[_currentAnimation][_currentFrameIndex], Color.White);

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
        /// Essentially plays the current Animation.
        /// Increments the frames of the current Animation according to the FPS.
        /// Also executes methods that handle occurences at certain points in the Animation (start, end, etc.)
        /// Also updates BoundingBox size.
        /// Needs to be called in Update method.
        /// </summary>
        /// <param name="gameTime"></param>
        private void PlayAnimation(GameTime gameTime)
        {
            // Adds time that has elapsed since the last Update
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // Change frame if timeElapsed is greater than timeToUpdate.
            if (_timeElapsed > _timeToUpdate)
            {
                // Resets the timer so that the desired Fps are achieved.
                _timeElapsed -= _timeToUpdate;

                // Increment frameIndex if Animation is not finished.
                if (_currentFrameIndex < _animations[_currentAnimation].Length - 1)
                    _currentFrameIndex++;

                else
                {
                    OnAnimationEnd(_currentAnimation);
                    _currentFrameIndex = 0;
                    OnAnimationStart(_currentAnimation);
                }
            }

            // Update BoundingBox size to fit current frame size.
            _boundingBox.Width = _animations[_currentAnimation][_currentFrameIndex].Width;
            _boundingBox.Height = _animations[_currentAnimation][_currentFrameIndex].Height;
        }

        /// <summary>
        /// Prepares the AnimatedSprite for playing the given Animation.
        /// </summary>
        /// <param name="name"></param>
        public void SetAnimation(EAnimation name)
        {
            // Makes sure we won't start a new annimation unless it differs from our current animation.
            if (_currentAnimation != name && _currentDirection.Equals(Direction.none))
            {
                _playingAnimation = true;
                _currentAnimation = name;
                _currentFrameIndex = 0;
                Fps = _animationFpsValues[name];
                OnAnimationStart(name);
            }
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
        /// <param name="name"></param>
        /// <param name="numFrames"></param>
        /// <param name="frameWidth"></param>
        /// <param name="frameHeight"></param>
        /// <param name="yRow"></param>
        /// <param name="indexFirstFrame"></param>
        /// <param name="offset"></param>
        /// <param name="fps"></param>
        /// <param name="onAnimationStart"></param>
        /// <param name="onAnimationEnd"></param>
        public void AddAnimation(EAnimation name, int numFrames, int frameWidth, int frameHeight, int yRow, int indexFirstFrame, 
            Vector2 offset, int fps, Action onAnimationStart = null, Action onAnimationEnd = null)
        {
            if (onAnimationStart == null)
                onAnimationStart = () => Console.WriteLine();
            if (onAnimationEnd == null)
                onAnimationEnd = () => Console.WriteLine();

            // Creates an array of rectangles (i.e. a new Animation).
            Rectangle[] animation = new Rectangle[numFrames];

            // Fills up the array of rectangles
            for (int i = 0; i < numFrames; i++)
                animation[i] = new Rectangle((i + indexFirstFrame) * frameWidth, yRow, frameWidth, frameHeight);

            // Store frames and offset in two different dictionaries. But both with same key (animation.)
            _animations.Add(name, animation);
            _offsets.Add(name, offset);
            _animationFpsValues.Add(name, fps);
            _onAnimationStartActions.Add(name, onAnimationStart);
            _onAnimationEndActions.Add(name, onAnimationEnd);
        }

        public void SetOnAnimationEnd(EAnimation name, Action action)
        {
            _onAnimationEndActions[name] = action;
        }

        public void SetOnAnimtionStart(EAnimation name, Action action)
        {
            _onAnimationStartActions[name] = action;
        }

        /// <summary>
        /// Executes the onAnimationStartAction for the given Animation.
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationStart(EAnimation name)
        {
            _onAnimationStartActions[name]();
        }

        /// <summary>
        /// Executes the onAnimationEndAction for the given Animation.
        /// </summary>
        /// <param name="name"></param>
        private void OnAnimationEnd(EAnimation name)
        {
            _onAnimationEndActions[name]();
        }

        /// <summary>
        /// Executes the onAnimationFrameAction for the given frame of the given Animation.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="frameIndex"></param>
        private void OnAnimationFrame(EAnimation name, int frameIndex)
        {

        }
    }
}
