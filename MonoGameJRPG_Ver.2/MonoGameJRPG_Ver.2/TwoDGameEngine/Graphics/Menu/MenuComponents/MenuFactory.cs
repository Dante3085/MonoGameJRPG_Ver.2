using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public static class MenuFactory
    {
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
    }
}