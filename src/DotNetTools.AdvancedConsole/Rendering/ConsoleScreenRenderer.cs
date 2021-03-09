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

        public void Render(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            Console.CursorVisible = false;
            var cursorLeft = Console.CursorLeft;
            var cursorTop = Console.CursorTop;
            var originalBackground = Console.BackgroundColor;
            var originalForeground = Console.ForegroundColor;

            InternalRender(screen, options);

            _lastCols = screen.Columns;
            _lastRows = screen.Rows;
            Console.BackgroundColor = originalBackground;
            Console.ForegroundColor = originalForeground;
            Console.SetCursorPosition(cursorLeft, cursorTop);
            Console.CursorVisible = screen.IsCursorVisible;
        }

        private void InternalRender(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            var changedPixelRanges = DetectChangedPixels(screen, options);

            foreach (var changedRange in changedPixelRanges)
            {
                Console.SetCursorPosition(changedRange.FromColumn, changedRange.Row);
                Console.BackgroundColor = changedRange.FirstPixel.BackgroundColor ?? DefaultBackgroundColor;
                Console.ForegroundColor = changedRange.FirstPixel.ForegroundColor ?? DefaultForegroundColor;
                Console.Write(changedRange.ConsoleContent);
            }
        }

        private IList<ChangedPixelRange> DetectChangedPixels(IConsoleScreen screen, ConsoleRenderOptions options)
        {
            var result = new List<ChangedPixelRange>();
            for (var row = 0; row < screen.Rows; row++)
                DetectChangedPixels(result, screen, row);
            return result;
        }

        private void DetectChangedPixels(List<ChangedPixelRange> result, IConsoleScreen screen, int row)
        {
            var rangeStart = 0;
            var pixel = screen.GetPixel(row, 0);
            var lastBg = pixel.BackgroundColor;
            var lastFg = pixel.ForegroundColor;
            var firstPixel = pixel;
            var anyChangesDetected = _lastRows != screen.Rows || _lastCols != screen.Columns;
            var sb = new StringBuilder(pixel.GetConsoleChar().ToString(), screen.Columns);

            for (var col = 1; col < screen.Columns; col++)
            {
                var point = new ConsolePoint(row, col);
                pixel = screen.GetPixel(point);

                if (pixel.BackgroundColor != lastBg || pixel.ForegroundColor != lastFg)
                {
                    if (anyChangesDetected)
                        result.Add(new ChangedPixelRange(firstPixel, row, rangeStart, col - 1, sb.ToString()));
                    firstPixel = pixel;
                    rangeStart = col;
                    lastBg = pixel.BackgroundColor;
                    lastFg = pixel.ForegroundColor;
                    sb.Clear();
                    anyChangesDetected = false;
                }
                sb.Append(pixel.GetConsoleChar());

                var oldPixel = _lastPixels.GetValueOrDefault(point);
                var pixelChanged = oldPixel == null || !oldPixel.Equals(pixel);
                anyChangesDetected = anyChangesDetected || pixelChanged;

                if (pixelChanged)
                {
                    var newPixel = new ConsoleScreenPixel(pixel);
                    if (oldPixel == null)
                        _lastPixels.Add(point, newPixel);
                    else
                        _lastPixels[point] = newPixel;
                }
            }

            if (anyChangesDetected)
                result.Add(new ChangedPixelRange(firstPixel, row, rangeStart, screen.Columns - 1, sb.ToString()));
        }

        private class ChangedPixelRange
        {
            public ChangedPixelRange(ConsoleScreenPixel firstPixel, int row, int fromColumn, int toColumn,
                string consoleContent)
            {
                FirstPixel = firstPixel;
                Row = row;
                FromColumn = fromColumn;
                ToColumn = toColumn;
                ConsoleContent = consoleContent;
            }

            public ConsoleScreenPixel FirstPixel { get; }
            public int Row { get; }
            public int FromColumn { get; }
            public int ToColumn { get; }
            public string ConsoleContent { get; }
            public int Length => ToColumn - FromColumn + 1;

            public override string ToString()
                => ConsoleContent;
        }
    }
}
