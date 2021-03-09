using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    /// <summary>
    /// Manages the console screen.
    /// </summary>
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
        /// Whether or not the cursor is visible on the screen
        /// </summary>
        bool IsCursorVisible { get; set; }

        /// <summary>
        /// Gets or sets the current column.
        /// </summary>
        int CurrentColumn { get; set; }

        /// <summary>
        /// Gets or sets the current row.
        /// </summary>
        int CurrentRow { get; set; }

        /// <summary>
        /// Gets the <see cref="ConsoleScreenPixel"/> at the specified row and column.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown in case of invalid row or column.</exception>
        ConsoleScreenPixel GetPixel(int row, int col);

        /// <summary>
        /// Updates the size of the console screen if it's dynamic.
        /// </summary>
        void ResizeScreenOnDemand();
    }
}
