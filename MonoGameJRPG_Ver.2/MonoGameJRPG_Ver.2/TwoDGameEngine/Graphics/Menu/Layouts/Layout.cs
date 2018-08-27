using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameJRPG_Ver._2.TwoDGameEngine.Graphics.Menu.MenuComponents.Layouts
{
    /// <summary>
    /// Describes essential parts of a layout. <para></para>
    /// At it's most essential a layout has to provide a Method that orders it's elements in a specific way (vetically, horizontally, etc.)
    /// </summary>
    public abstract class Layout : MenuElement
    {
        #region MemberVariables

        private int _x;
        private int _y;

        #endregion
        #region Constructors

        protected Layout(string name, int x = 0, int y = 0) : base(name)
        {
            _x = x;
            _y = y;
        }

        #endregion
        #region AbstractMethods

        public abstract List<MenuElement> Elements();
        public abstract int Offset { get; set; }
        public abstract void OrderElements();

        #endregion
    }
}
