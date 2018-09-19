using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine
{
    /// <summary>
    /// Base class for every kind of object in the game.
    /// </summary>
    public class GameObject
    {
        #region MemberVariables

        /// <summary>
        /// Stores number of existing GameObjects.
        /// </summary>
        private static int _numGameObjects;

        #endregion

        #region Properties

        public static int NumGameObjects
        {
            get { return _numGameObjects; }
        }

        #endregion

        /// <summary>
        /// Constructor. Increments _numGameObjects.
        /// </summary>
        public GameObject()
        {
            _numGameObjects++;            
        }

        /// <summary>
        /// Destructor / Finalizer. Decrements _numGameObjects.
        /// </summary>
        ~GameObject()
        {
            _numGameObjects--;
        }
    }
}