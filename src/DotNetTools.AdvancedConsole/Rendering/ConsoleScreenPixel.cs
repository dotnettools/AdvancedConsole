using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    /// <summary>
    /// Describes properties of one character of the console.
    /// </summary>
    public class ConsoleScreenPixel
    {
        public static readonly ConsoleScreenPixel Default = new ConsoleScreenPixel();

        public char Char { get; set; } = '\0';

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public void Clear()
        {
            Char = '\0';
            ForegroundColor = BackgroundColor = null;
        }
    }
}
