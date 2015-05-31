#region Using Statements

using System;
using System.IO;

#endregion

namespace TowerDefenceXNA
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
            try
            {
                using (var game = new Game1())
                    game.Run();
            }
            catch (Exception e)
            {
                string outputMessage = String.Format("message: {1}{0}" +
                                                     "sourse: {2}{0}" +
                                                     "stackTrace: {3}{0}{0}" +
                                                     "----------------------{0}{0}", Environment.NewLine, e.Message, e.Source, e.StackTrace);
                File.AppendAllText("logs.txt", outputMessage);
            }
        }
    }
#endif
}
