using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    /// <summary>
    /// Defines behavior for the console screen.
    /// </summary>
    public interface IConsoleController
    {
        /// <summary>
        /// Runs the controller logic.
        /// </summary>
        void Run(ConsoleControllerContext context);
    }
}
