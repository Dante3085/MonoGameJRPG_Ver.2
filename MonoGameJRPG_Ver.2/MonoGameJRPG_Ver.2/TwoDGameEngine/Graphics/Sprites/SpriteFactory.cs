using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Sprites
{
    public static class SpriteFactory
    {
        #region MenuElements

        public static AnimatedSprite NewGameButton()
        {
            Texture2D btnTexture = Contents.btnNewGame;
            if (btnTexture == null)
                throw new Exception("Contents.btnNewGame Texture not loaded!");

            AnimatedSprite newGameBtn = new AnimatedSprite("NewGameBtn", Contents.btnNewGame, new Vector2(100, 100), 5);
            newGameBtn.AddAnimation(EAnimation.Idle, 12, 48, 13, 0, 0, Vector2.Zero);
            return newGameBtn;
        }
        public static AnimatedSprite GlowingButton(Vector2 position)
        {
            Texture2D glowingButtonTex = Contents.glowingButton;
            if (glowingButtonTex == null)
                throw new Exception("Contents.glowingButton Texture not loaded!");

            AnimatedSprite glowingButton = new AnimatedSprite("GlowingButton", glowingButtonTex, position, 10);
            glowingButton.AddAnimation(EAnimation.Idle, 1, 120, 50, 0, 0, Vector2.Zero);
            glowingButton.AddAnimation(EAnimation.MouseHover, 14, 120, 50, 0, 0, Vector2.Zero);

            return glowingButton;
        }

        public static AnimatedSprite DiscoButton(Vector2 position)
        {
            Texture2D discoButtonTex = Contents.discoButton;
            if (discoButtonTex == null)
                throw new Exception("Contents.DiscoButton Texture not loaded!");

            AnimatedSprite discoButton = new AnimatedSprite("DiscoButton", discoButtonTex, position, 46);
            discoButton.AddAnimation(EAnimation.Idle, 1, 18, 18, 0, 0, Vector2.Zero);
            discoButton.AddAnimation(EAnimation.MouseHover, 36, 18, 18, 0, 0, Vector2.Zero);

            return discoButton;
        }
        public static AnimatedSprite BowlingBall()
        {
            Texture2D bowlingBallTexture = Contents.bowlingBall;
            if (bowlingBallTexture == null)
                throw new Exception("Contents.bowlingBall Texture not loaded!");

            AnimatedSprite bowlingBall = new AnimatedSprite("BowlingBall", bowlingBallTexture, new Vector2(0, 0), PlayerIndex.One, 20, new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S
            });
            bowlingBall.AddAnimation(EAnimation.Idle, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleLeft, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleUp, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleRight, 1, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.IdleDown, 1, 32, 32, 0, 0, Vector2.Zero);

            bowlingBall.AddAnimation(EAnimation.Left, 9, 32, 32, 32, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.Up, 9, 32, 32, 32, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.Right, 9, 32, 32, 0, 0, Vector2.Zero);
            bowlingBall.AddAnimation(EAnimation.Down, 9, 32, 32, 32, 0, Vector2.Zero);

            return bowlingBall;
        }

        #endregion
    }
}
