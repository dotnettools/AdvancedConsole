using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleControllerContext
    {
        public ConsoleControllerContext(Action<IConsoleScreen> renderer)
        {
            Render = renderer;
        }

        /// <summary>
        /// Renders a screen.
        /// </summary>
        public Action<IConsoleScreen> Render { get; }

        /// <summary>
        /// Gets or sets the next controller for the next cycle.
        /// A value of NULL means to keep using the same controller.
        /// </summary>
        public IConsoleController NextController { get; set; }

        /// <summary>
        /// Gets or sets whether or not the program should be terminated.
        /// </summary>
        public bool Terminated { get; set; }

        /// <summary>
        /// Sets the program to be terminated.
        /// </summary>
        public void Terminate()
            => Terminated = true;
    }
}
