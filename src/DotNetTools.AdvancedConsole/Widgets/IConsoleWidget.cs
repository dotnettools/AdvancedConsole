using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    public interface IConsoleWidget
    {
        /// <summary>
        /// Whether or not this widget should be rendered.
        /// </summary>
        bool IsVisible { get; set; }

        int Left { get; set; }
        int Top { get; set; }
        int Width { get; set; }
        int Height { get; set; }

        ConsoleColor? BackgroundColor { get; set; }
        ConsoleColor? ForegroundColor { get; set; }

        /// <summary>
        /// Updates the screen with current widget properties.
        /// </summary>
        void Update();
    }
}
