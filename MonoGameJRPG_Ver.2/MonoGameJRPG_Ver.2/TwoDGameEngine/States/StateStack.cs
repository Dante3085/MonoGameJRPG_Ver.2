using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.States
{
    public class StateStack
    {
        Dictionary<EState, State> _states;
        Stack<State> _stack = new Stack<State>();

        public StateStack(Dictionary<EState, State> states)
        {
            _states = states;
        }

        public void Update(GameTime gameTime)
        {
            _stack.Peek().Update(gameTime);
        }

        public void Render()
        {
            _stack.Peek().Render();
        }

        public void Push(EState state)
        {
            if (_stack.Count != 0)
            {
                // Return if IState that is on top of Stack is State to be pushed
                if (_stack.Peek().Equals(_states[state]))
                    return;
            }

            _stack.Push(_states[state]);
        }

        public State Pop()
        {
            State temp = _stack.Pop();

            // Stack can't be empty. Closing the 
            if (_stack.Count == 0)
                _stack.Push(temp);

            return temp;
        }
    }
}
