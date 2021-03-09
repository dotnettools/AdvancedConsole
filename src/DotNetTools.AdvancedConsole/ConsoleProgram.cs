using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole
{
    public class ConsoleProgram
    {
        private readonly ConsoleScreenRenderer _renderer;
        private IConsoleController _controller;

        public ConsoleProgram()
        {
            _renderer = new ConsoleScreenRenderer();
        }

        public ConsoleProgram(ConsoleScreenRenderer renderer)
        {
            _renderer = renderer;
        }

        public ConsoleColor DefaultBackgroundColor { get; set; } = Console.BackgroundColor;

        public ConsoleColor DefaultForegroundColor { get; set; } = Console.ForegroundColor;

        public IConsoleController Controller
        {
            get => _controller;
            set
            {
                if (_controller == value)
                    return;
                _controller = value;
            }
        }

        public bool Terminated { get; set; }

        public void Run()
        {
            if (Controller == null)
                throw new InvalidOperationException("Cannot run the program as no controller is specified.");

            while (true)
            {
                if (Controller == null || Terminated)
                    break;

                var context = new ConsoleControllerContext(Render);
                Controller.Run(context);

                if (context.Terminated)
                    break;

                if (context.NextController != null)
                    Controller = context.NextController;
            }

            Console.Clear();
        }

        private void Render(IConsoleScreen screen)
        {
            _renderer.Render(screen, new ConsoleRenderOptions
            {
                RenderingMode = ConsoleRenderingMode.Update,
            });
        }

        public void Terminate()
        {
            Terminated = true;
        }
    }
}
