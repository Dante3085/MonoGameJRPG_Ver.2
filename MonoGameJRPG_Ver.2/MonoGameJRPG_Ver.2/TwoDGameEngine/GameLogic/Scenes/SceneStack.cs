using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    public class SceneStack
    {
        Dictionary<EScene, Scene> _states;
        Stack<Scene> _stack = new Stack<Scene>();

        public SceneStack(Dictionary<EScene, Scene> states)
        {
            _states = states;
        }

        public void Update(GameTime gameTime)
        {
            _stack.Peek().Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _stack.Peek().Draw(spriteBatch);
        }

        public Scene Peek()
        {
            return _stack.Peek();
        }

        public void Push(EScene scene)
        {
            if (_stack.Count != 0)
            {
                // Return if IState that is on top of Stack is Scene to be pushed
                if (_stack.Peek().Equals(_states[scene]))
                    return;
            }

            _stack.Push(_states[scene]);
        }

        public Scene Pop()
        {
            Scene temp = _stack.Pop();

            // Stack can't be empty. 
            if (_stack.Count == 0)
                _stack.Push(temp);

            return temp;
        }
    }
}
