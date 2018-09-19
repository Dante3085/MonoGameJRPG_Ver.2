using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG_Ver._2.Characters;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu
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
            return new AnimatedMenuButton("glowingbutton", glowingButton,
                functionality: () => Game1.gameConsole.Log("Hallo, von Button"));
        }

        public static AnimatedMenuButton DiscoButton(Vector2 position)
        {
            AnimatedSprite discoButton = SpriteFactory.DiscoButton(position);
            return new AnimatedMenuButton("discobutton", discoButton,
                functionality: () => Game1.gameConsole.Log("Disco Button!"));
        }

        public static MenuButton RedButton(Vector2 position)
        {
            return new MenuButton("redbutton", Contents.redButtonNoHover, Contents.redButtonHover,
                functionality: () => Game1.gameConsole.Log("Red Button pressed"));
        }

        #endregion
        #region Menus

        public static Menu MainMenu(Point position)
        {
            return new Menu(new VBox("mainoptions", position.X, position.Y, 5, elements: new MenuElement[]
            {
                new Text("newgame", text: "New Game", functionality: () =>
                {
                    Game1.Game.FiniteStateMachine.Change(EState.TestLevelState);
                }),
                new Text("loadgame", text: "Load Game", functionality: null),
                new Text("console", text: "Console", functionality: () =>
                {
                    if (Game1.gameConsole.IsOpen)
                        Game1.gameConsole.Close();
                    else
                        Game1.gameConsole.Open(Keys.Tab);
                }),
                new Text("exitgame", text: "Exit Game", functionality: () => Game1.Game.Exit()),
            }));
        }

        public static Menu InventoryMenu(Point position, params Character[] characters)
        {
            VBox vbox = new VBox("vbox", position.X, position.Y, verticalOffset: 5);
            foreach (Character c in characters)
                vbox.Elements.Add(new CharacterInfo("characterInfo_" + c.Name, c));
            vbox.Elements.Add(new Text("mainMenuText", text: "MainMenu", functionality: () =>
            {
                Game1.Game.FiniteStateMachine.Change(EState.MainMenuState);
            }));

            Menu inventoryMenu = new Menu(vbox);
            return inventoryMenu;
        }
        public static HBox TestMenu(Vector2 position)
        {
            HBox testMenu = new HBox("testMenu", elements: new MenuElement[]
            {
                new VBox("mainoptions", 10, elements: new MenuElement[]
                {
                    new Text("vboxmainoptions", text: "vboxmainoptions"),
                    new Text("newgame", text: "New Game", functionality: null),
                    new Text("loadgame", text: "Load Game", functionality: null),
                    new Text("console", text: "Console", functionality: () =>
                    {
                        if (Game1.gameConsole.IsOpen)
                            Game1.gameConsole.Close();
                        else
                            Game1.gameConsole.Open(Keys.Tab);
                    }),
                    new Text("exitgame", text: "Exit Game", functionality: () => Game1.Game.Exit()),

                    new HBox("hbox2", horizontalOffset: 5, elements: new MenuElement[]
                    {
                        new Text("hbox2", text: "hbox2"),
                        GlowingButton(position),
                        GlowingButton(position),
                        GlowingButton(position),
                        RedButton(position),
                        GlowingButton(position),

                        new VBox("vbox2", elements: new MenuElement[]
                        {
                            new Text("vbox2", text: "Vbox2"),
                            RedButton(position),
                            RedButton(position),
                            RedButton(position),

                            new HBox("hbox3", elements: new MenuElement[]
                            {
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),
                                new Text("hbox3", text: "hbox3"),

                                new VBox("vbox4", verticalOffset: 10, elements: new MenuElement[]
                                {
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                    new Text("vbox4", text: "vbox4"),
                                }),
                            }),
                        }),
                    }),
                }),

                new VBox("vbox3", elements: new MenuElement[]
                {
                    new Text("vbox3", text: "vbox3"),
                    RedButton(position),
                    RedButton(position),
                    RedButton(position),
                    RedButton(position),
                }),
            });
            return testMenu;
        }

        #endregion
    }
}