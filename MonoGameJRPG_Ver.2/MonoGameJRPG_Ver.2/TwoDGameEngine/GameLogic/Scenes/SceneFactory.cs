using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.Scenes;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
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
            return new Scene(Contents.ff15Background, MenuFactory.MainMenu(Vector2.Zero, gameInstance));
        }

        public static Scene FirstLevelScene(Game1 gameInstance, params IEntity[] entities)
        {
            return new Scene(Contents.blueBackground, entities);
        }

        public static Scene InventoryScene(Game1 gameInstance)
        {
            throw new NotImplementedException();
        }

        public static Scene EmptyScene()
        {
            throw new NotImplementedException();
        }
    }
}
