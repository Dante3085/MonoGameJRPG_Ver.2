using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameJRPG.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public class FiniteStateMachine
    {
        #region MemberVariables

        /// <summary>
        /// Stores all States known to the FiniteStateMachine.
        /// </summary>
        private Dictionary<EState, State> _states;

        /// <summary>
        /// Stores a reference to the currenState of the FiniteStateMachine.
        /// </summary>
        private EState _currentState = EState.EmptyState;

        /// <summary>
        /// Stores how the FiniteStateMachine's States receive Input.
        /// Keyboard if true, else GamePad.
        /// </summary>
        private static bool _inputByKeyboard = true;

        #endregion

        #region Properties

        /// <summary>
        /// Stores how the FiniteStateMachine's States receive Input.
        /// Keyboard if true, else GamePad.
        /// </summary>
        public static bool InputByKeyboard => _inputByKeyboard;

        #endregion

        /// <summary>
        /// Constructs a FiniteStateMachine with the given States.
        /// </summary>
        /// <param name="states"></param>
        public FiniteStateMachine(Dictionary<EState, State> states)
        {
            _states = states;
        }

        /// <summary>
        /// Checks for InputType (Keyboard or GamePad).
        /// Updates the currentState of the FiniteStateMachine.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _inputByKeyboard = !InputManager.GamePadConnected();
            _states[_currentState].Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _states[_currentState].Draw(spriteBatch);
        }

        /// <summary>
        /// If the given State is known to the FiniteStateMachine, the currentState of the FiniteStateMachine
        /// is changed to the given State.
        /// </summary>
        /// <param name="state"></param>
        public void Change(EState state)
        {
            if (!_states.ContainsKey(state))
                throw new FiniteStateMachineException("@FiniteStateMachine.Change(" + state + "): " 
                                                      + state + " is not known to this FiniteStateMachine!");

            if (!_states[_currentState].Next.Contains(state))
                throw new FiniteStateMachineException("@FiniteStateMachine.Change(" + state + "): "
                                                      + _currentState + " does not allow for a Change from it to "
                                                      + state);

            _states[_currentState].OnExit();
            _currentState = state;
            _states[_currentState].OnEnter();
        }

        /// <summary>
        /// If the given State is not already known to the FiniteStateMachine, the given State is added
        /// to the FiniteStateMachine.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public void Add(EState name, State state)
        {
            if (_states.ContainsKey(name))
                throw new FiniteStateMachineException("@FiniteStateMachine.Add(" + name + "): " 
                                                      + name + " is already known to this FiniteStateMachine!");
            _states.Add(name, state);
        }

        /// <summary>
        /// If the given State is known to the FiniteStateMachine, the given State is removed from the
        /// FiniteStateMachine.
        /// </summary>
        /// <param name="state"></param>
        public void Remove(EState state)
        {
            if (!_states.ContainsKey(state))
                throw new FiniteStateMachineException("@FiniteStateMachine.Remove(" + state + "): " 
                                                      + state + " is not known to this FiniteStateMachine!");
            _states.Remove(state);
        }
    }
}
