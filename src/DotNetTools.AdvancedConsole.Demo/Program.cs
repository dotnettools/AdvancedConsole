using System;

namespace DotNetTools.AdvancedConsole.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = ConsoleScreen.CreateForWindow();
            var p = screen.GetPixel(2, 4);
            p.Char = 'X';
            p.BackgroundColor = ConsoleColor.Blue;
            p.ForegroundColor = ConsoleColor.Yellow;
            screen.Render();
        }
    }
}
