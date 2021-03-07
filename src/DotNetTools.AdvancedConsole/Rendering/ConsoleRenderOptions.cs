using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    /// <summary>
    /// Render options
    /// </summary>
    public class ConsoleRenderOptions
    {
        /// <summary>
        /// Default <see cref="ConsoleRenderOptions"/>
        /// </summary>
        public static readonly ConsoleRenderOptions Default = new ConsoleRenderOptions();

        /// <summary>
        /// Gets or sets the render mode.
        /// </summary>
        public ConsoleRenderingMode RenderingMode { get; set; } = ConsoleRenderingMode.Full;
    }
}
