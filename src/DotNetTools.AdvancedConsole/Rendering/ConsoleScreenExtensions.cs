using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public static class ConsoleScreenExtensions
    {
        /// <summary>
        /// Gets the <see cref="ConsoleScreenPixel"/> at the specified row and column.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in case of invalid row or column.</exception>
        public static ConsoleScreenPixel GetPixel(this IConsoleScreen screen, ConsolePoint point)
            => screen.GetPixel(point.Row, point.Column);

        /// <summary>
        /// Renders the screen with default options.
        /// </summary>
        public static void Render(this IConsoleScreen screen)
            => screen.Render(ConsoleRenderOptions.Default);

        /// <summary>
        /// Clears a rectangular part of the screen.
        /// </summary>
        public static void Clear(this IConsoleScreen screen, int colStart, int colEnd, int rowStart, int rowEnd)
        {
            if (colEnd < colStart)
                Swap(ref colStart, ref colEnd);
            if (rowEnd < rowStart)
                Swap(ref rowStart, ref rowEnd);

            for (var row = rowStart; row <= rowEnd; row++)
                for (var col = colStart; col <= colEnd; col++)
                {
                    var pixel = screen.GetPixel(row, col);
                    pixel.Clear();
                }
        }


        /// <summary>
        /// Clears the screen
        /// </summary>
        public static void Clear(this IConsoleScreen screen)
            => screen.Clear(0, screen.Columns - 1, 0, screen.Rows - 1);

        private static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
