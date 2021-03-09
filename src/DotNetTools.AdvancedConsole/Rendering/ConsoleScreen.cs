using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleScreen : IConsoleScreen
    {
        public static readonly ConsoleScreen Blank = CreateForWindow();

        private readonly Func<(int rows, int columns)> _sizeCallback;
        private int _columns, _rows;
        private ConsoleScreenPixel[][] _pixels;

        public ConsoleScreen(int columns, int rows)
        {
            _columns = columns;
            _rows = rows;
            UpdateSize();
        }

        public ConsoleScreen() : this(0, 0)
        {
        }

        public ConsoleScreen(Func<(int rows, int columns)> sizeCallback)
            : this(sizeCallback().columns, sizeCallback().rows)
        {
            _sizeCallback = sizeCallback;
            ResizeScreenOnDemand();
        }

        public int Columns
        {
            get => _columns;
            set
            {
                if (_columns == value)
                    return;
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                _columns = value;
                UpdateSize();
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                if (_rows == value)
                    return;
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                _rows = value;
                UpdateSize();
            }
        }

        public bool IsCursorVisible { get; set; } = true;

        public int CurrentColumn { get; set; }

        public int CurrentRow { get; set; }

        public void ResizeScreenOnDemand()
        {
            if (_sizeCallback == null)
                return;
            var (rows, columns) = _sizeCallback();
            Columns = columns;
            Rows = rows;
        }

        public ConsoleScreenPixel GetPixel(int row, int col)
        {
            var rowPixel = _pixels[row];
            if (rowPixel[col] == null)
                rowPixel[col] = new ConsoleScreenPixel();
            CurrentRow = row;
            CurrentColumn = col;
            return rowPixel[col];
        }

        private void UpdateSize()
        {
            var oldPixels = _pixels;
            _pixels = new ConsoleScreenPixel[_rows][];
            for (var i = 0; i < _rows; i++)
            {
                var oldRow = oldPixels != null && oldPixels.Length > i ? oldPixels[i] : null;
                _pixels[i] = new ConsoleScreenPixel[_columns];
                if (oldRow != null)
                    Array.Copy(oldRow, _pixels[i], Math.Clamp(_columns, 0, oldRow.Length));
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ConsoleScreen"/> with the size of the console window.
        /// </summary>
        public static ConsoleScreen CreateForWindow()
            => new ConsoleScreen(() => (Console.WindowHeight, Console.WindowWidth));
    }
}
