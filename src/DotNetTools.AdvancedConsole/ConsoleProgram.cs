using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleProgram
    {
        private IConsoleScreen _screen;
        private readonly ConsoleScreenRenderer _renderer;

        public ConsoleProgram()
        {
        }

        public ConsoleProgram(IConsoleScreen startupScreen)
        {
            _screen = startupScreen;
        }

        public ConsoleColor DefaultBackgroundColor { get; set; } = Console.BackgroundColor;

        public ConsoleColor DefaultForegroundColor { get; set; } = Console.ForegroundColor;

        public IConsoleScreen ActiveScreen
        {
            get => _screen;
            set
            {
                if (_screen == value)
                    return;
                _screen = value;
                Invalidate();
            }
        }

        public void Run()
        {
            IConsoleScreen screen = null;

            while (true)
            {
            }
        }

        public void Invalidate()
        {
        }
    }
}
