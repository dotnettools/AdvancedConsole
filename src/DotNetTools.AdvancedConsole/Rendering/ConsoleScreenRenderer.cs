using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleScreenRenderer
    {
        private readonly Dictionary<ConsolePoint, ConsoleScreenPixel> _lastPixels = new Dictionary<ConsolePoint, ConsoleScreenPixel>();
        private int _lastRows, _lastCols;

        public ConsoleScreenRenderer()
        {
        }

        public ConsoleColor DefaultBackgroundColor { get; set; } = Console.BackgroundColor;
        public ConsoleColor DefaultForegroundColor { get; set; } = Console.ForegroundColor;

        public void RenderPixel(int row, int col, ConsoleScreenPixel pixel)
        {
            Console.SetCursorPosition(col, row);
            RenderPixel(pixel);
        }

        public void RenderPixel(ConsoleScreenPixel pixel)
        {
            Console.BackgroundColor = pixel.BackgroundColor ?? DefaultBackgroundColor;
            Console.ForegroundColor = pixel.ForegroundColor ?? DefaultForegroundColor;

            char ch = pixel.Char;
            if (pixel.Char == '\0')
                ch = ' ';
            Console.Write(ch);
        }

        public void Render(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            Console.CursorVisible = false;
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            var originalBackground = Console.BackgroundColor;
            var originalForeground = Console.ForegroundColor;

            var invalidate = ShouldInvalidate(screen, options);

            if (invalidate)
                FullRender(screen, options);
            else
                UpdateRender(screen, options);

            _lastCols = screen.Columns;
            _lastRows = screen.Rows;
            Console.BackgroundColor = originalBackground;
            Console.ForegroundColor = originalForeground;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = screen.IsCursorVisible;
        }

        private void UpdateRender(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            for (var row = 0; row < screen.Rows; row++)
                for (var col = 0; col < screen.Columns; col++)
                {
                    var point = new ConsolePoint(row, col);
                    var pixel = screen.GetPixel(point);
                    _lastPixels.TryGetValue(point, out var oldPixel);

                    if (oldPixel != null && oldPixel.Equals(pixel))
                        continue;

                    var renderedPixel = new ConsoleScreenPixel(pixel);
                    if (_lastPixels.ContainsKey(point))
                        _lastPixels[point] = renderedPixel;
                    else
                        _lastPixels.Add(point, renderedPixel);

                    RenderPixel(row, col, pixel);
                }
        }

        private void FullRender(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            _lastPixels.Clear();
            for (var row = 0; row < screen.Rows; row++)
                for (var col = 0; col < screen.Columns; col++)
                {
                    var point = new ConsolePoint(row, col);
                    var pixel = screen.GetPixel(row, col);
                    _lastPixels.Add(point, new ConsoleScreenPixel(pixel));

                    RenderPixel(row, col, pixel);
                }
        }

        private bool ShouldInvalidate(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            if (_lastRows != screen.Rows || _lastCols != screen.Columns)
                return true;
            return options.RenderingMode == ConsoleRenderingMode.Full;
        }
    }
}
