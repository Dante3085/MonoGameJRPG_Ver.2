using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.Layouts
{
    public abstract class Layout : MenuElement
    {
        public abstract int Offset { get; set; }
        public abstract List<MenuElement> Elements();
        public abstract void OrderElements();
    }
}
