/* Author: Iker Ruiz Arnauda
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AsyncSimpleServer.Server;
using AsyncSimpleServer.Logging;

namespace AsyncSimpleServer
{
    class Program
    {
        #region properties
        /// <summary>
        /// Used for uptime calculations.
        /// </summary>
        public static readonly DateTime StartupTime = DateTime.Now;

        /// <summary>
        /// VisualKB Server instance.
        /// </summary>
        public static VskbServer VskbServer;

        /// <summary>
        /// VisualKB Logger Instance
        /// </summary>
        private static readonly Logger Logger = LogManager.CreateLogger();
        #endregion

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler; // Watch for any unhandled exceptions.

            Console.ForegroundColor = ConsoleColor.Yellow;
            // Print ASCII Banner.
            PrintBanner();
            // Set back color.
            Console.ResetColor(); 

            // Initialize Logging facility.
            InitLoggers();
            
            Logger.Info("VisualKB Server v{0} warming-up...", Assembly.GetExecutingAssembly().GetName().Version);

            // Initialize Server
            StartVskbServer();

            //User input, this keeps the server running unless the user hits the escape key.
            //Todo: Handle commands.
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.ReadLine();
            }
        }

        #region Logging facility
        /// <summary>
        /// Inits logging facility and loggers.
        /// </summary>
        private static void InitLoggers()
        {   
            //Enable logger by default.
            LogManager.Enabled = true; 

            foreach (var targetConfig in LogConfig.Instance.Targets)
            {
                if (!targetConfig.Enabled)
                    continue;

                LogTarget target = null;

                switch (targetConfig.Target.ToLower())
                {
                    case "console":
                        target = new ConsoleTarget(targetConfig.MinimumLevel, targetConfig.MaximumLevel,
                                                   targetConfig.IncludeTimeStamps);
                        break;
                    case "file":
                        target = new FileTarget(targetConfig.FileName, targetConfig.MinimumLevel,
                                                targetConfig.MaximumLevel, targetConfig.IncludeTimeStamps,
                                                targetConfig.ResetOnStartup);
                        break;
                }

                if (target != null)
                    LogManager.AttachLogTarget(target);
            }
        }

        #endregion

        #region Unhandled Exception Emitter
        /// <summary>
        /// Unhandled exception emitter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (e.IsTerminating)
                Logger.FatalException(ex, "VisualKB server terminating because of unhandled exception.");
            else
                Logger.ErrorException(ex, "Caught unhandled exception.");

            Console.ReadLine();
        }
        #endregion

        #region StartServer
        public static bool StartVskbServer()
        {
            if (VskbServer != null) return false;

            VskbServer = new VskbServer();
            return true;
        }
        #endregion

        #region ASCII Banner
        /// <summary>
        /// Prints an info banner.
        /// </summary>
        private static void PrintBanner()
        {
            Console.WriteLine(@"_  _ _ ____ _  _ ____ _    _  _ ___     ____ ____ ____ _  _ ____ ____ ");
            Console.WriteLine(@"|  | | [__  |  | |__| |    |_/  |__]    [__  |___ |__/ |  | |___ |__/ ");
            Console.WriteLine(@" \/  | ___] |__| |  | |___ | \_ |__]    ___] |___ |  \  \/  |___ |  \ ");
            Console.WriteLine();
        }
        #endregion
    }
}
