using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine
{
    /// <summary>
    /// Holds Textures and SpriteFonts for loading and usage.
    /// </summary>
    public static class Contents
    {
        #region Texture2Ds
    
        #region BackgroundImages

        public static Texture2D space;
        public static Texture2D samurai;
        public static Texture2D blueBackground;

        #endregion
        #region Menu

        public static Texture2D btnNoHover;
        public static Texture2D btnHover;

        #endregion
        #region Characters

        public static Texture2D warrior;

        #endregion

        #endregion

        #region SpriteFonts

        public static SpriteFont fontNoHover;
        public static SpriteFont fontHover;

        #endregion

        /// <summary>
        /// Uses given ContentManager to load all Content declared inside Contents class.
        /// </summary>
        /// <param name="c"></param>
        public static void LoadAll(ContentManager c)
        {

        }


    }
}