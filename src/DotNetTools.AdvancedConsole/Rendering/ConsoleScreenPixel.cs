using DotNetTools.AdvancedConsole.Internal;
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

        public ConsoleScreenPixel()
        {
        }

        public ConsoleScreenPixel(char @char, ConsoleColor? foregroundColor = null, ConsoleColor? backgroundColor = null)
        {
            Char = @char;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public ConsoleScreenPixel(ConsoleScreenPixel pixel)
        {
            Char = pixel.Char;
            ForegroundColor = pixel.ForegroundColor;
            BackgroundColor = pixel.BackgroundColor;
        }

        public char Char { get; set; } = '\0';

        public ConsoleColor? ForegroundColor { get; set; }

        public ConsoleColor? BackgroundColor { get; set; }

        public bool IsEmpty
            => Char == '\0' && ForegroundColor == null && BackgroundColor == null;

        public void Clear()
        {
            Char = '\0';
            ForegroundColor = BackgroundColor = null;
        }

        /// <summary>
        /// Converts <see cref="Char"/> of '\0' to space.
        /// </summary>
        public char GetConsoleChar()
        {
            switch (Char)
            {
                case '\0':
                    return ' ';
                default:
                    return Char;
            }
        }

        public override int GetHashCode()
            => HashCodeHelper.GetHashCode(Char, ForegroundColor, BackgroundColor);

        public override bool Equals(object obj)
        {
            if (obj is ConsoleScreenPixel pixel)
            {
                return Char == pixel.Char && ForegroundColor == pixel.ForegroundColor
                    && BackgroundColor == pixel.BackgroundColor;
            }
            return base.Equals(obj);
        }
    }
}
