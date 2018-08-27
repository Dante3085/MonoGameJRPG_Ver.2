using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents.MenuComponents
{
    /// <summary>
    /// Factory class used to get pre-defined instances of Menu-related objects.
    /// </summary>
    public static class MenuFactory
    {
        #region MenuButtons

        public static AnimatedMenuButton GlowingButton(Vector2 position)
        {
            AnimatedSprite glowingButton = SpriteFactory.GlowingButton(position);
            return new AnimatedMenuButton(glowingButton, () => Game1.gameConsole.Log("Hallo, von Button"));
        }

        public static AnimatedMenuButton DiscoButton(Vector2 position)
        {
            AnimatedSprite discoButton = SpriteFactory.DiscoButton(position);
            return new AnimatedMenuButton(discoButton, () => Game1.gameConsole.Log("Disco Button!"));
        }

        public static MenuButton RedButton(Vector2 position)
        {
            return new MenuButton(Contents.redButtonNoHover, Contents.redButtonHover,
                function: () => Game1.gameConsole.Log("Red Button pressed"));
        }

        #endregion

        #region Menus

        public static Menu MainMenu(Vector2 position, Game1 gameInstance)
        {
            Menu mainMenu = new Menu("MainMenu", new List<MenuElement>()
            {
                new VBox(10, 10, 5, elements: new MenuElement[]
                {
                    new Text("New Game", functionality: () => Game1.gameConsole.Log("NewGame")),
                    new Text("Load Game", functionality: () => Game1.gameConsole.Log("LoadGame")),
                    new Text("Console", functionality: () =>
                    {
                        if (!Game1.gameConsole.IsOpen)
                            Game1.gameConsole.Open(null);
                        else
                            Game1.gameConsole.Close();
                    }),
                    new Text("Exit Game", functionality: () => gameInstance.ExitGame()),
                }),



        }, 0, 0);
            return mainMenu;
        }

        #endregion
    }
}