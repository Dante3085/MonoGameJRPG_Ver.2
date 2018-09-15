using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.GameLogic.States
{
    public class EmptyState : State
    {

        public EmptyState(List<EState> next) :base(new List<IEntity>(), next, "EmptyState")
        {

        }
    }
}
