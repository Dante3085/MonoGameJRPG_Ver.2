using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public class FiniteStateMachineException : Exception
    {
        public FiniteStateMachineException() : base()
        {
            
        }

        public FiniteStateMachineException(string msg) : base(msg)
        { }
    }
}
