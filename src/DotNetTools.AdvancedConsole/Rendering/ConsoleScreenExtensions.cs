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

        /// <summary>
        /// Writes a text at the specified coordinates.
        /// </summary>
        public static void Write(this IConsoleScreen screen, string text, int row, int col,
            Action<ConsolePoint, ConsoleScreenPixel> pixelUpdater = null)
        {
            foreach (var ch in text)
            {
                var point = new ConsolePoint(row, col);
                var pixel = screen.GetPixel(point);
                pixel.Char = ch;
                pixelUpdater?.Invoke(point, pixel);

                if (++col >= screen.Columns)
                {
                    col = 0;
                    row++;
                }
            }

            screen.CurrentRow = row;
            screen.CurrentColumn = col;
        }

        /// <summary>
        /// Writes a text at the specified coordinates with the specified colors.
        /// </summary>
        public static void Write(this IConsoleScreen screen, string text, int row, int col,
            ConsoleColor? backgroundColor, ConsoleColor? foregroundColor)
            => screen.Write(text, row, col, (_, pix) =>
                    {
                        pix.BackgroundColor = backgroundColor;
                        pix.ForegroundColor = foregroundColor;
                    });

        /// <summary>
        /// Writes a text at current cursor position.
        /// </summary>
        public static void Write(this IConsoleScreen screen, string text,
            Action<ConsolePoint, ConsoleScreenPixel> pixelUpdater = null)
            => screen.Write(text, screen.CurrentRow, screen.CurrentColumn, pixelUpdater);

        /// <summary>
        /// Invokes <paramref name="painter"/> for each of the pixels of the rectangle formed by the given parameters.
        /// </summary>
        public static void Paint(this IConsoleScreen screen, int startRow, int endRow, int startCol, int endCol,
            Action<ConsoleScreenPixel> painter)
        {
            for (var row = startRow; row <= endRow; row++)
                for (var col = startCol; col <= endCol; col++)
                {
                    var pixel = screen.GetPixel(row, col);
                    painter(pixel);
                }
        }

        /// <summary>
        /// Fills a rectangle.
        /// </summary>
        public static void Paint(this IConsoleScreen screen, int startRow, int endRow, int startCol, int endCol,
            ConsoleColor? bg, ConsoleColor? fg, char? ch = null)
            => Paint(screen, startRow, endRow, startCol, endCol, pix =>
            {
                pix.BackgroundColor = bg;
                pix.ForegroundColor = fg;
                if (ch.HasValue)
                    pix.Char = ch.Value;
            });

        private static void Swap<T>(ref T a, ref T b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
