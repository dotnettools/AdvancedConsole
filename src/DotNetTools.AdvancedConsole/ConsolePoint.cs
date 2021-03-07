using DotNetTools.AdvancedConsole.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    /// <summary>
    /// Represents a pair of row and column indices.
    /// </summary>
    public class ConsolePoint
    {
        public ConsolePoint(int row, int column)
        {
            Row = row;
            Column = column;
        }

        /// <summary>
        /// Gets the row index.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Gets the column index.
        /// </summary>
        public int Column { get; }

        public override int GetHashCode()
            => HashCodeHelper.GetHashCode(Row, Column);

        public override bool Equals(object obj)
        {
            if (obj is ConsolePoint point)
                return Row == point.Row && Column == point.Column;
            return base.Equals(obj);
        }
    }
}
