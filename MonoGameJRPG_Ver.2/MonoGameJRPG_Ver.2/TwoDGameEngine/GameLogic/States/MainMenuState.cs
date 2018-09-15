using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public class MainMenuState : State
    {
        private Texture2D _background;
        
        public MainMenuState(Texture2D background, List<IEntity> entities, List<EState> next, string name) 
            : base(entities, next, name)
        {
            _background = background;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
