using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleScreenRenderer
    {
        private readonly IConsoleScreen _screen;
        private readonly Dictionary<ConsolePoint, ConsoleScreenPixel> _lastPixels = new Dictionary<ConsolePoint, ConsoleScreenPixel>();
        private int _lastRows, _lastCols;

        public ConsoleScreenRenderer(IConsoleScreen screen)
        {
            _screen = screen;
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
            if (pixel.Char == '\0')
                return;

            Console.BackgroundColor = pixel.BackgroundColor ?? DefaultBackgroundColor;
            Console.ForegroundColor = pixel.ForegroundColor ?? DefaultForegroundColor;
            Console.Write(pixel.Char);
        }

        public void Render(ConsoleRenderOptions options)
        {
            var invalidate = ShouldInvalidate(options);

            if (invalidate)
                FullRender(options);
            else
                UpdateRender(options);

            _lastCols = _screen.Columns;
            _lastRows = _screen.Rows;
        }

        private void UpdateRender(ConsoleRenderOptions options)
        {
            for (var row = 0; row < _screen.Rows; row++)
                for (var col = 0; col < _screen.Columns; col++)
                {
                    var point = new ConsolePoint(row, col);
                    var pixel = _screen.GetPixel(point);
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

        private void FullRender(ConsoleRenderOptions options)
        {
            _lastPixels.Clear();
            for (var row = 0; row < _screen.Rows; row++)
                for (var col = 0; col < _screen.Columns; col++)
                {
                    var point = new ConsolePoint(row, col);
                    var pixel = _screen.GetPixel(row, col);
                    _lastPixels.Add(point, new ConsoleScreenPixel(pixel));

                    RenderPixel(row, col, pixel);
                }
        }

        private bool ShouldInvalidate(ConsoleRenderOptions options)
        {
            if (_lastRows != _screen.Rows || _lastCols != _screen.Columns)
                return true;
            return options.RenderingMode == ConsoleRenderingMode.Full;
        }
    }
}
