using System;

namespace MonoGameJRPG_Ver._2
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Game1 game = Game1.Game)
                game.Run();
        }
    }
#endif
}
