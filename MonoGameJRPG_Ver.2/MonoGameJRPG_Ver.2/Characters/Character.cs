using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.Characters
{
    /// <summary>
    /// The Character class represents playable and non-playable Figures/Characters in the game.<para></para>
    /// RevengeSystem: If a Character gets continuously bashed by enemy attacks without having the chance to act against that or
    ///                just being completely overwhelmed, he can force himself out of the situation by executing a RevengeSkill.
    /// </summary>
    public class Character : GameObject, IEntity
    {
        #region MemberVariables

        /// <summary>
        /// AnimatedSprite / visual representation of the Character.
        /// </summary>
        private AnimatedSprite _animatedSprite;

        /// <summary>
        /// Name of the Character.
        /// </summary>
        private string _name;

        #region Stats

        /// <summary>
        /// CurrentHp. Bigger or equal to 0 and smaller or equal to MaxHp. Describes the current amount of Hitpoints the Character has.
        /// </summary>
        private int _currentHp;

        /// <summary>
        /// MaxHp. Bigger or equal to 0. Describes the maximum amount of Hitpoints the Character can have.
        /// </summary>
        private int _maxHp;

        /// <summary>
        /// Describes if the Character is alive or not. <para></para>
        /// Alive, if currentHP > 0. Dead, if currentHP = 0.
        /// </summary>
        private bool _isAlive = true;

        /// <summary>
        /// CurrentMp. Bigger or equal to 0 and smaller or equal to MaxMP. Describes the current amount of Magicpoints the Character has.
        /// </summary>
        private int _currentMp;

        /// <summary>
        /// MaxMp. Bigger or equal to 0. Describes maximum amount of Magicpoints the Character can have.
        /// </summary>
        private int _maxMp;

        /// <summary>
        /// Describes the Character's physical capability when it comes to using physical attacks.
        /// </summary>
        private int _strength;

        /// <summary>
        /// Describes the Character's physical capability when it comes to defending against physical attacks.
        /// </summary>
        private int _defence;

        /// <summary>
        /// Describes the Character's physical capability when it comes to avoiding physical attacks.
        /// </summary>
        private int _agility;

        /// <summary>
        /// Describes the Character's mental capability when it comes to using and defending against magic attacks.
        /// </summary>
        private int _wit;

        /// <summary>
        /// Describes how fast the Character will be able to act in Combat.
        /// </summary>
        private int _speed;

        /// <summary>
        /// Describes the threshold at which, if revengeValue reaches it, the Character is able to or will execute a RevengeSkill.
        /// </summary>
        private int _revengeThreshold;

        /// <summary>
        /// Describes the current value at which revenge is at.
        /// </summary>
        private int _revengeValue;

        /// <summary>
        /// Describes the current level of the Character.
        /// </summary>
        private int _lvl;

        #endregion

        /// <summary>
        /// Stores if the Character is currently attacking.
        /// // TODO: It is not clear if this is necessary for the JRPG !
        /// </summary>
        private bool _isAttacking = false;

        /// <summary>
        /// Stores what Action the Character will execute when asked to execute it's current Action.
        /// // TODO: It is not clear if this is necessary for the JRPG !
        /// </summary>
        private EAction _currentAction = EAction.Cleave;

        #region Input

        /// <summary>
        /// Stores the Character's KeyboardInput.
        /// </summary>
        private KeyboardInput _keyboardInput;

        /// <summary>
        /// Stores the Character's GamePadInput.
        /// </summary>
        private GamePadInput _gamePadInput;

        /// <summary>
        /// Stores if the Character is/can controlled by player.
        /// </summary>
        private bool _isPlayerControlled;

        #endregion

        /// <summary>
        /// Stores all the Actions that are registered on this Character.
        /// </summary>
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
        public int RevengeValue { get => _revengeThreshold; set => _revengeThreshold = value; }
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        public string Name { get => _name; set => _name = value; }
        public int CurrentHp { get => _currentHp; set => _currentHp = value; }
        public int MaxHp { get => _maxHp; set => _maxHp = value; }
        public int CurrentMp { get => _currentMp; set => _currentMp = value; }
        public int MaxMp { get => _maxMp; set => _maxMp = value; }
        public bool IsAttacking { get => _isAttacking; set => _isAttacking = value; }
        public bool IsPlayerControlled { get => _isPlayerControlled; }

        #endregion
        #region Methods


        public Character(String name = "NoName", int maxHp = 0, int maxMp = 0, int strength = 0, int defence = 0, int wit = 0,
            int agility = 0, int speed = 0, bool isPlayerControlled = false, KeyboardInput keyboardInput = null, 
            GamePadInput gamePadInput = null, AnimatedSprite animatedSprite = null)
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
            _animatedSprite.IsPlayerControlled = this.IsPlayerControlled;

            SetUp_Items();
            SetUp_PhysicalSkills();
            SetUp_RevengeSkills();
            SetUp_Magics();
        }

        //public void Interact(IInteractable other)
        //{
        //    HandleInteraction();
        //    other.HandleInteraction();
        //}

        //public void HandleInteraction()
        //{

        //}

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
                _keyboardInput = KeyboardInput.None();
            }

            if (_gamePadInput == null)
                _gamePadInput = GamePadInput.Default();

            if (_animatedSprite == null)
            {
                //_animatedSprite = new AnimatedSprite(this._name, Vector2.Zero, PlayerIndex.One, )
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _animatedSprite.Draw(spriteBatch);
        }

        /// <summary>
        /// Use every tick. Handles necessary updates for a Character.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="characters"></param>
        public void Update(GameTime gameTime)
        {
            _animatedSprite.Update(gameTime);

            // TODO: PlayerIndex muss an AnimSprite gegeben werden.
            if (InputManager.GamePadConnected())
                HandleGamePadInput(gameTime);
            else
                HandleKeyboardInput(gameTime);

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
            {
                if (this.AnimatedSprite.CollidesWith(c.AnimatedSprite))
                    HandleCollision(c);
                else
                    c.AnimatedSprite.Prop_DrawInteractionPrompt = false;
            }
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

            if (target.AnimatedSprite.IsInteractable)
                target.AnimatedSprite.Prop_DrawInteractionPrompt = true;
        }

        private void UseCurrentAction(Character target)
        {
            _actions[_currentAction].ExecuteAction(target);
        }

        /// <summary>
        /// Returns a String with all relevant Information about the Character.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name + "[CurrentHp: " + _currentHp + "|MaxHp" + _maxHp + 
                   ", IsAlive" + _isAlive + ", CurrentMp: " + _currentMp + 
                   "|MaxMp:" + _maxMp + ", Strength: " + _strength + 
                   "Defence: " + _defence + ", Agility: " + _agility + 
                   ", Wit: " + _wit + ", Spe\ned: " + _speed + 
                   ", RevengeThreshold: " + _revengeThreshold + ", RevengeValue: " + 
                   _revengeValue + ", Lvl: " + _lvl + "]";
        }

        #region HandleInput

        /// <summary>
        /// Handles Input given to the Character by Keyboard.
        /// </summary>
        public void HandleKeyboardInput(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// Handles Input given to the Character given by GamePad.
        /// </summary>
        public void HandleGamePadInput(GameTime gameTime)
        {
            
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
        #endregion
    }
}
