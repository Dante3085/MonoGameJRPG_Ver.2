using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public static class StateFactory
    {
        public static MainMenuState DefaultMainMenuState()
        {
            List<IEntity> entities = new List<IEntity>()
            {
                new VBox("mainMenü", 10, 10, 5, null, new MenuElement[]
                {
                    new Text("", text: "New Game", functionality: () => Game1.gameConsole.Log("New Game")), 
                })
            };
            MainMenuState mainMenu = new MainMenuState(Contents.ff15Background, entities, null, "mainmenu");
            return mainMenu;
        }
    }
}
