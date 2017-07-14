using System;
using System.Threading;
using IRTaktiks.Listener;

namespace IRTaktiks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Check if the TUIO messages must be listened.
            if (IRTSettings.Default.ListenTUIO)
            {
                TuioDump listener = new TuioDump();
                ThreadPool.QueueUserWorkItem(listener.Run, null);
            }

			// Create the game and start it.
			using (IRTGame game = new IRTGame())
			{
                game.Run();
			}
        }
    }
}

