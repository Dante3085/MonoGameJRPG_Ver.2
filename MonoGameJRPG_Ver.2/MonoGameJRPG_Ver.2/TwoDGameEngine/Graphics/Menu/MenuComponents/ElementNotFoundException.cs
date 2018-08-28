using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException() : base()
        { }

        public ElementNotFoundException(string message) : base(message)
        { }
    }
}
