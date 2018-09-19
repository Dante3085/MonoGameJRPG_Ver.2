using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public abstract class State
    {
        #region MemberVariables

        /// <summary>
        /// Stores the entities of the State.
        /// </summary>
        private List<IEntity> _entities;

        /// <summary>
        /// Stores references to States that can be reached from this State.
        /// </summary>
        private List<EState> _next;


        private Action _keyboardHandler;

        private Action _gamePadHandler;

        private Action _inputHandler;

        /// <summary>
        /// Stores the name(purpose) of the State.
        /// </summary>
        private string _name;

        #endregion

        #region Properties

        public List<EState> Next => _next;

        public Action KeyboardInput
        {
            get => _keyboardHandler;
            set => _keyboardHandler = value;
        }

        public Action GamePadInput
        {
            get => _gamePadHandler;
            set => _gamePadHandler = value;
        }

        public string Name => _name;

        #endregion

        /// <summary>
        /// Constructs a State with the given entities name.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="name"></param>
        /// <param name="keyboardHandler"></param>
        /// <param name="gamePadHandler"></param>
        /// 
        public State(List<IEntity> entities, List<EState> next, Action keyboardHandler = null, 
            Action gamePadHandler = null, string name = "NO_NAME_STATE")
        {
            _entities = entities;
            _next = next;
            _keyboardHandler = keyboardHandler;
            _gamePadHandler = gamePadHandler;
            _name = name;

            if (_keyboardHandler == null)
                _keyboardHandler = () => { };
            
            if (_gamePadHandler == null)
                _gamePadHandler = () => { };

            _inputHandler = InputManager.GamePadConnected() ? _gamePadHandler : _keyboardHandler;
        }

        /// <summary>
        /// Updates every entity of the State.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (IEntity e in _entities)
                e.Update(gameTime);
            _inputHandler();
        }

        /// <summary>
        /// Draws drawable entities.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (IEntity e in _entities)
                if (e is IDrawable)
                    ((IDrawable)e).Draw(spriteBatch);
        }

        /// <summary>
        /// Behaviour for when the FiniteStateMachine changes it's currentState to this State.
        /// </summary>
        public virtual void OnEnter()
        {
            Util.Log("Entered '" + _name + "' State.");
            Console.WriteLine("Entered '" + _name + "' State.");
            Game1.gameConsole.Log("Entered '" + _name + "' State.");
        }

        /// <summary>
        /// Behaviour for when the FiniteStateMachine changes it's currentState from this State to another
        /// State.
        /// </summary>
        public virtual void OnExit()
        {
            Util.Log("Exited '" + _name + "' State.");
            Console.WriteLine("Exited '" + _name + "' State.");
            Game1.gameConsole.Log("Exited '" + _name + "' State.");
        }
    }
}
