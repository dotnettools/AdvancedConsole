using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public interface IConsoleScreen
    {
        /// <summary>
        /// Total number of columns
        /// </summary>
        int Columns { get; set; }

        /// <summary>
        /// Total number of rows
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// Gets the <see cref="ConsoleScreenPixel"/> at the specified row and column.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in case of invalid row or column.</exception>
        ConsoleScreenPixel GetPixel(int col, int row);

        /// <summary>
        /// Renders the screen
        /// </summary>
        void Render();
    }
}
