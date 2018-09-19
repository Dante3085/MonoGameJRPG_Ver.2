using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public static class StateFactory
    {
        public static SceneState DefaultMainMenuState()
        {
            List<IEntity> entities = new List<IEntity>()
            {
                MenuFactory.MainMenu(new Point(10, 10))
            };
            SceneState scene = new SceneState(Contents.ff15Background, entities, new List<EState>()
            {
                EState.TestLevelState,
            }, name: "mainmenu");
            return scene;
        }

        public static SceneState InventoryState(params Character[] chars)
        {
            
           
            return new SceneState(Contents.blackBackground, new List<IEntity>()
            {
                MenuFactory.InventoryMenu(new Point(10, 10), chars)
            }, new List<EState>()
            {
                EState.MainMenuState,
                EState.TestLevelState,
            }, () =>
            {
                if (InputManager.OnKeyDown(Keys.I))
                    Game1.Game.FiniteStateMachine.Change(EState.TestLevelState);
            }, name: "InventoryState");
        }

        public static SceneState TestLevelState(params IEntity[] entities)
        {
            return new SceneState(Contents.blueBackground, new List<IEntity>(entities), 
                new List<EState>()
            {
                EState.InventoryState
            }, () =>
            {
                if (InputManager.OnKeyDown(Keys.I))
                    Game1.Game.FiniteStateMachine.Change(EState.InventoryState);
            }, name: "TestLevelState");
        }
    }
}
