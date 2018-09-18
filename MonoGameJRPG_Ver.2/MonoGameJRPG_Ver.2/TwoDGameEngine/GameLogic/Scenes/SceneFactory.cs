using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes
{
    /// <summary>
    /// Class for quickly getting common Scene configurations.
    /// </summary>
    public static class SceneFactory
    {
        public static Scene MainMenuScene(Game1 gameInstance)
        {
            return new Scene("MainMenuScene", Contents.ff15Background, entities: MenuFactory.MainMenu(new Point(10, 10)));
        }

        public static Scene FirstLevelScene(Game1 gameInstance, params IEntity[] entities)
        {
            CollisionManager c = new CollisionManager();

            foreach (IEntity e in entities)
                if (e is Character)
                    c.Collidables.Add(((Character)e).AnimatedSprite);

            return new Scene("FirstLevelScene", Contents.whiteBackground, keyboardHandler: () =>
                {
                    //if (InputManager.OnKeyDown(Keys.I))
                    //    gameInstance._sceneStack.Push(EScene.InventoryScene);
                }, gamePadHandler: () =>
                {
                    //if (InputManager.OnButtonDown(Buttons.Start))
                    //    gameInstance._sceneStack.Push(EScene.InventoryScene);
                },collisionManager: c,
                entities: entities);
        }

        public static Scene InventoryScene(Game1 gameInstance, params Character[] characters)
        {
            return new Scene("InventoryScene", Contents.blackBackground, keyboardHandler: () =>
            {
                //if (InputManager.OnKeyDown(Keys.I))
                //    gameInstance._sceneStack.Pop();
            }, gamePadHandler: () =>
            {
                //if (InputManager.OnButtonDown(Buttons.Start))
                //    gameInstance._sceneStack.Pop();
            },entities: MenuFactory.InventoryMenu(new Point(10, 10), characters));
        }

        public static Scene EmptyScene()
        {
            throw new NotImplementedException();
        }
    }
}
