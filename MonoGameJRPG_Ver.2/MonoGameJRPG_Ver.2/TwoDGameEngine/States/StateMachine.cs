using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.States
{
    public class StateMachine
    {
        Dictionary<EState, State> _states = new Dictionary<EState, State>();
        State _currentState = new EmptyState();

        public void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
        }

        public void Render()
        {
            _currentState.Render();
        }

        public void Change(EState state /*, params*/)
        {
            _currentState.OnExit();
            _currentState = _states[state];
            _currentState.OnEnter(/*params*/);
        }

        public void Add(EState stateName, State state)
        {
            _states[stateName] = state;
        }
    }
}
