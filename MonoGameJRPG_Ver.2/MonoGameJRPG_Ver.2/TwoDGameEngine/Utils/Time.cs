using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Utils
{
    /// <summary>
    /// Capsules _seconds, _minutes and _hours in a class.
    /// </summary>
    public class Time
    {
        #region MemberVariables
        private int _seconds = 0;
        private int _minutes = 0;
        private int _hours = 0;

        /// <summary>
        /// Is raised by gameTime.ElapsedGameTime.TotalSeconds amount each Update() call.
        /// Used to check if 1 second passed, then increment _seconds.
        /// </summary>
        private double _tracker = 0;
        #endregion

        #region Properties
        public int Seconds { get => _seconds; }
        public int Minutes { get => _minutes; }
        public int Hours { get => _hours; }
        #endregion

        public void Update(GameTime gameTime)
        {
            _tracker += gameTime.ElapsedGameTime.TotalSeconds;

            if (_tracker >= 1)
            {
                _seconds++;
                _tracker = 0;
            }

            if (_seconds != 60)
                return;
            _minutes++;
            _seconds = 0;

            if (_minutes != 60)
                return;
            _hours++;
            _minutes = 0;
        }

        public override string ToString()
        {
            return _hours + ":" + _minutes + ":" + _seconds;
        }
    }
}
