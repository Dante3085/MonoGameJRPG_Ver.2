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

    /// <summary>
    /// Factory class used to get pre-defined instances of Sprite objects.
    /// </summary>
    public static class SpriteFactory
    {
        #region Characters

        public static AnimatedSprite BowlingBall(Vector2 position)
        {
            Texture2D bowlingBallTexture = Contents.bowlingBall;
            if (bowlingBallTexture == null)
                throw new Exception("Contents.bowlingBall Texture not loaded!");

            AnimatedSprite bowlingBall = new AnimatedSprite("BowlingBall", bowlingBallTexture, position, PlayerIndex.One, 20, new KeyboardInput()
            {
                Left = Keys.A,
                Up = Keys.W,
                Right = Keys.D,
                Down = Keys.S
            });
            bowlingBall.AddAnimation(EAnimation.Idle, 1, 32, 32, 0, 0, Vector2.Zero, 1);
            bowlingBall.AddAnimation(EAnimation.IdleLeft, 1, 32, 32, 0, 0, Vector2.Zero, 1);
            bowlingBall.AddAnimation(EAnimation.IdleUp, 1, 32, 32, 0, 0, Vector2.Zero, 1);
            bowlingBall.AddAnimation(EAnimation.IdleRight, 1, 32, 32, 0, 0, Vector2.Zero, 1);
            bowlingBall.AddAnimation(EAnimation.IdleDown, 1, 32, 32, 0, 0, Vector2.Zero, 1);

            bowlingBall.AddAnimation(EAnimation.Left, 9, 32, 32, 32, 0, Vector2.Zero, 10);
            bowlingBall.AddAnimation(EAnimation.Up, 9, 32, 32, 32, 0, Vector2.Zero, 10);
            bowlingBall.AddAnimation(EAnimation.Right, 9, 32, 32, 0, 0, Vector2.Zero, 10);
            bowlingBall.AddAnimation(EAnimation.Down, 9, 32, 32, 32, 0, Vector2.Zero, 10);

            return bowlingBall;
        }
        public static AnimatedSprite Swordsman(Vector2 position)
        {
            Texture2D swordsmanTex = Contents.swordsman;

            AnimatedSprite swordsman = new AnimatedSprite
                ("Swordsman", swordsmanTex, position, PlayerIndex.One, 5, isInteractable: true, keyboardInput: KeyboardInput.None());

            // Idle Animation that is played on startup
            swordsman.AddAnimation(EAnimation.Idle, 4, 48, 43, 433, 0, new Vector2(0, 0), 5);

            // Idle Animations for each direction.
            swordsman.AddAnimation(EAnimation.IdleLeft, 4, 48, 43, 528, 4, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleUp, 4, 48, 43, 528, 0, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleRight, 4, 48, 43, 480, 0, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleDown, 4, 48, 43, 433, 0, new Vector2(0, 0), 5);

            // Movement Animations for each direction.
            swordsman.AddAnimation(EAnimation.Left, 8, 50, 50, 100, 0, new Vector2(0, 0), 12);
            swordsman.AddAnimation(EAnimation.Up, 12, 50, 50, 50, 0, new Vector2(0, 0), 15);
            swordsman.AddAnimation(EAnimation.Right, 8, 50, 50, 100, 8, new Vector2(0, 0), 12);
            swordsman.AddAnimation(EAnimation.Down, 12, 50, 50, 0, 0, new Vector2(0, 0), 15);

            swordsman.PlayAnimation(EAnimation.Idle);

            // Attack Animations for each direction.
            //swordsman.AddAnimation(9, 150, 0, "AttackDown", 70, 80, new Vector2(0, 0));
            //swordsman.AddAnimation(9, 230, 0, "AttackUp", 70, 80, new Vector2(-13, -27));
            //swordsman.AddAnimation(9, 310, 0, "AttackLeft", 70, 70, new Vector2(-30, -5));
            //swordsman.AddAnimation(9, 380, 0, "AttackRight", 70, 70, new Vector2(+15, -5));

            return swordsman;
        }
        public static AnimatedSprite NPC(Vector2 position)
        {
            Texture2D swordsmanTex = Contents.swordsman;

            AnimatedSprite swordsman = new AnimatedSprite
                ("NPC", swordsmanTex, position, PlayerIndex.One, 5, isInteractable: true, keyboardInput: KeyboardInput.None());

            // Idle Animation that is played on startup
            swordsman.AddAnimation(EAnimation.Idle, 4, 48, 43, 433, 0, new Vector2(0, 0), 5);

            // Idle Animations for each direction.
            swordsman.AddAnimation(EAnimation.IdleLeft, 4, 48, 43, 528, 4, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleUp, 4, 48, 43, 528, 0, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleRight, 4, 48, 43, 480, 0, new Vector2(0, 0), 5);
            swordsman.AddAnimation(EAnimation.IdleDown, 4, 48, 43, 433, 0, new Vector2(0, 0), 5);

            // Movement Animations for each direction.
            swordsman.AddAnimation(EAnimation.Left, 8, 50, 50, 100, 0, new Vector2(0, 0), 12);
            swordsman.AddAnimation(EAnimation.Up, 12, 50, 50, 50, 0, new Vector2(0, 0), 15);
            swordsman.AddAnimation(EAnimation.Right, 8, 50, 50, 100, 8, new Vector2(0, 0), 12);
            swordsman.AddAnimation(EAnimation.Down, 12, 50, 50, 0, 0, new Vector2(0, 0), 15);

            swordsman.PlayAnimation(EAnimation.Idle);

            // Attack Animations for each direction.
            //swordsman.AddAnimation(9, 150, 0, "AttackDown", 70, 80, new Vector2(0, 0));
            //swordsman.AddAnimation(9, 230, 0, "AttackUp", 70, 80, new Vector2(-13, -27));
            //swordsman.AddAnimation(9, 310, 0, "AttackLeft", 70, 70, new Vector2(-30, -5));
            //swordsman.AddAnimation(9, 380, 0, "AttackRight", 70, 70, new Vector2(+15, -5));

            return swordsman;
        }

        #endregion
        #region MenuElements

        public static AnimatedSprite NewGameButton(Vector2 position)
        {
            Texture2D btnTexture = Contents.btnNewGame;
            if (btnTexture == null)
                throw new Exception("Contents.btnNewGame Texture not loaded!");

            AnimatedSprite newGameBtn = new AnimatedSprite("NewGameBtn", Contents.btnNewGame, position, 5);
            newGameBtn.AddAnimation(EAnimation.Idle, 12, 48, 13, 0, 0, Vector2.Zero, 10);
            return newGameBtn;
        }
        public static AnimatedSprite GlowingButton(Vector2 position)
        {
            Texture2D glowingButtonTex = Contents.glowingButton;
            if (glowingButtonTex == null)
                throw new Exception("Contents.glowingButton Texture not loaded!");

            AnimatedSprite glowingButton = new AnimatedSprite("GlowingButton", glowingButtonTex, position, 12);
            glowingButton.AddAnimation(EAnimation.Idle, 1, 120, 50, 0, 0, Vector2.Zero, 1);
            glowingButton.AddAnimation(EAnimation.MouseHover, 14, 120, 50, 0, 0, Vector2.Zero, 10);

            return glowingButton;
        }
        public static AnimatedSprite DiscoButton(Vector2 position)
        {
            Texture2D discoButtonTex = Contents.discoButton;
            if (discoButtonTex == null)
                throw new Exception("Contents.DiscoButton Texture not loaded!");

            AnimatedSprite discoButton = new AnimatedSprite("DiscoButton", discoButtonTex, position, 60);
            discoButton.AddAnimation(EAnimation.Idle, 1, 18, 18, 0, 0, Vector2.Zero, 1);
            discoButton.AddAnimation(EAnimation.MouseHover, 36, 18, 18, 0, 0, Vector2.Zero, 10);

            return discoButton;
        }
        public static AnimatedSprite Heart(Vector2 position)
        {
            AnimatedSprite heart = new AnimatedSprite("Heart", Contents.heart, position, 1);
            heart.AddAnimation(EAnimation.Idle, 1, 16, 16, 0, 0, Vector2.Zero, 1);
            heart.AddAnimation(EAnimation.Hurt, 1, 16, 16, 0, 1, Vector2.Zero, 1);
            heart.AddAnimation(EAnimation.MouseHover, 1, 16, 16, 0, 0, Vector2.Zero, 1);
            return heart;
        }

        #endregion
    }
}
