using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.Characters
{
    /// <summary>
    /// The Character class represents playable and non-playable Figures/Characters in the game.
    /// A Character has certain Attributes that describe it's role in Combat or other aspects of the game.
    /// A Character is able to perform 6 different types of actions in Combat.
    /// - Use a skill
    /// - Just attack
    /// - Defend
    /// - Use Magic
    /// - Use Item
    /// - End Turn
    /// </summary>
    public class Character : GameObject, IInputable
    {
        #region MemberVariables
        private AnimatedSprite _animatedSprite;

        private string _name;

        #region Stats
        private int _currentHp;
        private int _maxHp;

        private int _currentMp;
        private int _maxMp;

        private int _strength;
        private int _defence;
        private int _wit;
        private int _agility;
        private int _speed;
        private int _revengeValue;

        private int _lvl;
        #endregion

        private bool _isAlive = true;

        private bool _isAttacking = false;

        private EAction _currentAction = EAction.Cleave;

        #region Input
        private KeyboardInput _keyboardInput;
        private GamePadInput _gamePadInput;

        private KeyboardState _previousKeyboardState = Keyboard.GetState();
        private GamePadState _previousGamePadState = GamePad.GetState(PlayerIndex.One);

        private bool _isPlayerControlled;
        #endregion

        private Dictionary<EAction, AAction> _actions = new Dictionary<EAction, AAction>();
        #endregion

        #region Properties
        public AnimatedSprite AnimatedSprite
        {
            get { return _animatedSprite; }
            set { _animatedSprite = value; }
        }

        public int Lvl => _lvl;
        public int Strength { get => _strength; set => _strength = value; }
        public int Defence { get => _defence; set => _defence = value; }
        public int Wit { get => _wit; set => _wit = value; }
        public int Agility { get => _agility; set => _agility = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int RevengeValue { get => _revengeValue; set => _revengeValue = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        public string Name { get => _name; set => _name = value; }
        public int CurrentHp { get => _currentHp; set => _currentHp = value; }
        public int MaxHp { get => _maxHp; set => _maxHp = value; }
        public int CurrentMp { get => _currentMp; set => _currentMp = value; }
        public int MaxMp { get => _maxMp; set => _maxMp = value; }
        public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
        public bool IsPlayerControlled { get => _isPlayerControlled; }
        #endregion

        public Character(String name = "NoName", int maxHp = 0, int maxMp = 0, int strength = 0, int defence = 0, int wit = 0,
            int agility = 0, int speed = 0, bool isPlayerControlled = false, KeyboardInput keyboardInput = null, GamePadInput gamePadInput = null, AnimatedSprite animatedSprite = null)
        {
            _name = name;

            _currentHp = maxHp;
            _maxHp = maxHp;

            _currentMp = maxMp;
            _maxMp = maxMp;

            _strength = strength;
            _defence = defence;
            _wit = wit;
            _agility = agility;
            _speed = speed;

            _isPlayerControlled = isPlayerControlled;
            _keyboardInput = keyboardInput;
            _gamePadInput = gamePadInput;

            _animatedSprite = animatedSprite;

            // This statement has to be after all variable assignments.
            HandleConstructorDefaults();

            _animatedSprite.KeyboardInput = this._keyboardInput;
            _animatedSprite.GamePadInput = this._gamePadInput;

            SetUp_Items();
            SetUp_PhysicalSkills();
            SetUp_RevengeSkills();
            SetUp_Magics();
        }

        /// <summary>
        /// Handles certain Default Parameters of Constructor.
        /// Example: KeyboardInput and GamePadInput can't be null. For user convenience they are set to default layouts
        /// when nothing was passed through constructor.
        /// </summary>
        private void HandleConstructorDefaults()
        {
            // NO KEYBOARDINPUT PASSED
            if (_keyboardInput == null)
            {
                _keyboardInput = new KeyboardInput()
                {
                    Left = Keys.A,
                    Up = Keys.W,
                    Right = Keys.D,
                    Down = Keys.S,
                    Interact = Keys.Space
                };
            }

            // NO GAMEPADINPUT PASSED
            if (_gamePadInput == null)
            {
                _gamePadInput = new GamePadInput()
                {
                    Left = Buttons.LeftThumbstickLeft,
                    Up = Buttons.LeftThumbstickUp,
                    Right = Buttons.LeftThumbstickRight,
                    Down = Buttons.LeftThumbstickDown,
                    Interact = Buttons.A
                };
            }

            if (_animatedSprite == null)
            {
                //_animatedSprite = new AnimatedSprite(this._name, Vector2.Zero, PlayerIndex.One, )
            }
        }

        /// <summary>
        /// Use every tick. Handles necessary updates for a Character.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="characters"></param>
        public void Update(GameTime gameTime, List<Character> characters)
        {
            _animatedSprite.Update(gameTime);

            // TODO: PlayerIndex muss an AnimSprite gegeben werden.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsConnected)
                HandleGamePadInput(gamePadState);
            else
                HandleKeyboardInput(Keyboard.GetState());

            if (characters != null)
                CheckCollisions(characters);

            // TODO: Verkapseln. Sorgt dafür, dass _isAttacking auf false gesetzt wird, wenn die Angriffsanimation vorbei ist.
            if (!_animatedSprite.PlayingAnimation)
                _isAttacking = false;

            CheckCharacterStatus();
        }

        private void CheckCharacterStatus()
        {
            if (!_isAlive)
                Respawn();
        }

        private void Respawn()
        {
            _animatedSprite._position.X = 500;
            _animatedSprite._position.Y = 300;

            _currentHp = _maxHp;

            _isAlive = true;
        }

        /// <summary>
        /// Checks if this Character collides with any of the characters in the passed list.
        /// If yes calls HandleCollision().
        /// </summary>
        /// <param name="characters"></param>
        private void CheckCollisions(List<Character> characters)
        {
            foreach (Character c in characters)
                if (this.AnimatedSprite.CollidesWith(c.AnimatedSprite))
                    HandleCollision(c);
        }

        /// <summary>
        /// Handles Collisions, i.e. decides what will happen on collision.
        /// </summary>
        /// <param name="target"></param>
        private void HandleCollision(Character target)
        {
            if (_isAttacking)
            {
                _isAttacking = false;
                UseCurrentAction(target);
            }
        }

        private void UseCurrentAction(Character target)
        {
            _actions[_currentAction].ExecuteAction(target);
        }

        public override string ToString()
        {
            return _name + "[" + _currentHp + "|" + _maxHp + "]";
        }

        #region HandleInput
        public void HandleKeyboardInput(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(_keyboardInput.Attack) && _previousKeyboardState.IsKeyUp(_keyboardInput.Attack))
                _isAttacking = true;

            _previousKeyboardState = keyboardState;
        }

        public void HandleGamePadInput(GamePadState gamePadState)
        {
            if (gamePadState.IsButtonDown(_gamePadInput.Attack) && _previousGamePadState.IsButtonUp(_gamePadInput.Attack))
                _isAttacking = true;

            _previousGamePadState = gamePadState;
        }
        #endregion

        #region SetUpMethods
        private void SetUp_Items()
        {
            _actions[EAction.HealthPotion] = AAction.HealthPotion();
            _actions[EAction.ManaPotion] = AAction.ManaPotion();
        }

        private void SetUp_Magics()
        {
            _actions[EAction.Fire] = AAction.Fire();
        }

        private void SetUp_PhysicalSkills()
        {
            _actions[EAction.Cleave] = AAction.Cleave();
        }

        private void SetUp_RevengeSkills()
        {
            _actions[EAction.Counter] = AAction.Counter();
            _actions[EAction.Break] = AAction.Break();
        }
        #endregion
    }
}
