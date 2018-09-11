using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameJRPG.TwoDGameEngine.Input;
using MonoGameJRPG_Ver._2.TwoDGameEngine.Utils;

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

            swordsman.SetAnimation(EAnimation.Idle);

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

            swordsman.SetAnimation(EAnimation.Idle);

            // Attack Animations for each direction.
            //swordsman.AddAnimation(9, 150, 0, "AttackDown", 70, 80, new Vector2(0, 0));
            //swordsman.AddAnimation(9, 230, 0, "AttackUp", 70, 80, new Vector2(-13, -27));
            //swordsman.AddAnimation(9, 310, 0, "AttackLeft", 70, 70, new Vector2(-30, -5));
            //swordsman.AddAnimation(9, 380, 0, "AttackRight", 70, 70, new Vector2(+15, -5));

            return swordsman;
        }

        public static AnimatedSprite Adventurer(Vector2 position)
        {
            AnimatedSprite adventurer = new AnimatedSprite("adventurer", Contents.adventurer, position, PlayerIndex.One, fps: 6, keyboardInput: KeyboardInput.Default(), 
                gamePadInput: GamePadInput.Default(), isInteractable: true);

            adventurer.AddAnimation(EAnimation.Idle, 4, 50, 37, 0, 0, Vector2.Zero, 4, 
                () => Console.WriteLine("Idle start"), () => Console.WriteLine("Idle end"));
            adventurer.AddAnimation(EAnimation.IdleLeft, 4, 50, 37, 0, 0, Vector2.Zero, 4,
                () => Console.WriteLine("IdleLeft start"), () => Console.WriteLine("IdleLeft end"));
            adventurer.AddAnimation(EAnimation.IdleUp, 4, 50, 37, 0, 0, Vector2.Zero, 4,
                () => Console.WriteLine("IdleUp start"), () => Console.WriteLine("IdleUp end"));
            adventurer.AddAnimation(EAnimation.IdleRight, 4, 50, 37, 0, 0, Vector2.Zero, 4,
                () => Console.WriteLine("IdleRight start"), () => Console.WriteLine("IdleRight end"));
            adventurer.AddAnimation(EAnimation.IdleDown, 4, 50, 37, 0, 0, Vector2.Zero, 4,
                () => Console.WriteLine("IdleDown start"), () => Console.WriteLine("IdleDown end"));

            adventurer.AddAnimation(EAnimation.Left, 5, 50, 37, 37, 1, Vector2.Zero, 5);
            adventurer.AddAnimation(EAnimation.Up, 5, 50, 37, 37, 1, Vector2.Zero, 5);
            adventurer.AddAnimation(EAnimation.Right, 5, 50, 37, 37, 1, Vector2.Zero, 5);
            adventurer.AddAnimation(EAnimation.Down, 5, 50, 37, 37, 1, Vector2.Zero, 5);

            adventurer.AddAnimation(EAnimation.MeleeLeft, 7, 50, 37, 222, 0, Vector2.Zero, 7,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.IdleLeft));
            adventurer.AddAnimation(EAnimation.MeleeUp, 7, 50, 37, 222, 0, Vector2.Zero, 7,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.IdleUp));
            adventurer.AddAnimation(EAnimation.MeleeRight, 7, 50, 37, 222, 0, Vector2.Zero, 7,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.IdleRight));
            adventurer.AddAnimation(EAnimation.MeleeDown, 7, 50, 37, 222, 0, Vector2.Zero, 7,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.IdleDown));

            adventurer.AddAnimation(EAnimation.Melee1, 7, 50, 37, 222, 0, Vector2.Zero, 8, 
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.Idle));
            adventurer.AddAnimation(EAnimation.Melee2, 4, 50, 37, 259, 0, Vector2.Zero, 8,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.Idle));
            adventurer.AddAnimation(EAnimation.Melee3, 3, 50, 37, 259, 4, Vector2.Zero, 4,
                onAnimationEnd: () => adventurer.SetAnimation(EAnimation.Idle));

            return adventurer;
        }

        #endregion
        #region MenuElements

        public static AnimatedSprite NewGameButton(Vector2 position)
        {
            Texture2D btnTexture = Contents.btnNewGame;
            if (btnTexture == null)
                throw new Exception("Contents.btnNewGame Texture not loaded!");

            AnimatedSprite newGameBtn = new AnimatedSprite("NewGameBtn", Contents.btnNewGame, position, fps: 5);
            newGameBtn.AddAnimation(EAnimation.Idle, 12, 48, 13, 0, 0, Vector2.Zero, 10);
            return newGameBtn;
        }
        public static AnimatedSprite GlowingButton(Vector2 position)
        {
            Texture2D glowingButtonTex = Contents.glowingButton;
            if (glowingButtonTex == null)
                throw new Exception("Contents.glowingButton Texture not loaded!");

            AnimatedSprite glowingButton = new AnimatedSprite("GlowingButton", glowingButtonTex, position, fps: 12);
            glowingButton.AddAnimation(EAnimation.Idle, 1, 120, 50, 0, 0, Vector2.Zero, 1);
            glowingButton.AddAnimation(EAnimation.MouseHover, 14, 120, 50, 0, 0, Vector2.Zero, 10);

            return glowingButton;
        }
        public static AnimatedSprite DiscoButton(Vector2 position)
        {
            Texture2D discoButtonTex = Contents.discoButton;
            if (discoButtonTex == null)
                throw new Exception("Contents.DiscoButton Texture not loaded!");

            AnimatedSprite discoButton = new AnimatedSprite("DiscoButton", discoButtonTex, position, fps: 60);
            discoButton.AddAnimation(EAnimation.Idle, 1, 18, 18, 0, 0, Vector2.Zero, 1);
            discoButton.AddAnimation(EAnimation.MouseHover, 36, 18, 18, 0, 0, Vector2.Zero, 10);

            return discoButton;
        }
        public static AnimatedSprite Heart(Vector2 position)
        {
            AnimatedSprite heart = new AnimatedSprite("Heart", Contents.heart, position, fps: 1);
            heart.AddAnimation(EAnimation.Idle, 1, 16, 16, 0, 0, Vector2.Zero, 1);
            heart.AddAnimation(EAnimation.Hurt, 1, 16, 16, 0, 1, Vector2.Zero, 1);
            heart.AddAnimation(EAnimation.MouseHover, 1, 16, 16, 0, 0, Vector2.Zero, 1);
            return heart;
        }

        #endregion
    }
}
