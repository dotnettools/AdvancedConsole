using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleScreen : IConsoleScreen
    {
        public static readonly ConsoleScreen Blank = CreateForWindow();

        private readonly ConsoleScreenRenderer _defaultRenderer;
        private int _columns, _rows;
        private ConsoleScreenPixel[][] _pixels;

        public ConsoleScreen() : this(0, 0)
        {
        }

        public ConsoleScreen(int columns, int rows)
        {
            _defaultRenderer = new ConsoleScreenRenderer();
            _columns = columns;
            _rows = rows;
            UpdateSize();
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

        public ConsoleScreenPixel GetPixel(int row, int col)
        {
            var rowPixel = _pixels[row];
            if (rowPixel[col] == null)
                rowPixel[col] = new ConsoleScreenPixel();
            CurrentRow = row;
            CurrentColumn = col;
            return rowPixel[col];
        }

        public void Render(ConsoleRenderOptions options)
            => _defaultRenderer.Render(this, options);

        private void UpdateSize()
        {
            var oldPixels = _pixels;
            _pixels = new ConsoleScreenPixel[_rows][];
            for (var i = 0; i < _rows; i++)
            {
                var oldRow = oldPixels?[i];
                _pixels[i] = new ConsoleScreenPixel[_columns];
                if (oldRow != null)
                    Array.Copy(oldRow, _pixels[i], Math.Clamp(_columns, 0, oldRow.Length));
            }

            Render(new ConsoleRenderOptions { RenderingMode = ConsoleRenderingMode.Full });
        }

        /// <summary>
        /// Creates a new instance of <see cref="ConsoleScreen"/> with the size of the console window.
        /// </summary>
        public static ConsoleScreen CreateForWindow()
            => new ConsoleScreen(Console.WindowWidth, Console.WindowHeight);
    }
}
