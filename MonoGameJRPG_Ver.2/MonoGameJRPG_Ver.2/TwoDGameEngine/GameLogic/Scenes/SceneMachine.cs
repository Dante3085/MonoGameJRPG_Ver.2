using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    public class SceneMachine
    {
        Dictionary<EScene, Scene> _states = new Dictionary<EScene, Scene>();
        Scene _currentScene = SceneFactory.EmptyScene();

        public void Update(GameTime gameTime)
        {
            _currentScene.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScene.Draw(spriteBatch);
        }

        public void Change(EScene scene /*, params*/)
        {
            _currentScene.OnExit();
            _currentScene = _states[scene];
            _currentScene.OnEnter(/*params*/);
        }

        public void Add(EScene sceneName, Scene scene)
        {
            _states[sceneName] = scene;
        }
    }
}
