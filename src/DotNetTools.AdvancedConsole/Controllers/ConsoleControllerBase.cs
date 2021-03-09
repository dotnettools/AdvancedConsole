using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Controllers
{
    public abstract class ConsoleControllerBase : IConsoleController
    {
        public ConsoleControllerBase(IConsoleScreen screen, ConsoleScreenRenderer renderer)
        {
            Screen = screen;
            Widgets = new ConsoleWidgetHost(screen, renderer);
            Renderer = renderer;
        }

        public ConsoleControllerBase(IConsoleScreen screen)
        {
            Screen = screen;
            Widgets = new ConsoleWidgetHost(screen);
        }

        public ConsoleControllerBase(ConsoleScreenRenderer renderer) : this(ConsoleScreen.CreateForWindow(), renderer)
        {
        }

        public ConsoleControllerBase() : this(ConsoleScreen.CreateForWindow())
        {
        }

        protected ConsoleWidgetHost Widgets { get; }
        protected IConsoleScreen Screen { get; }
        protected ConsoleScreenRenderer Renderer { get; }

        public abstract void Run(ConsoleControllerContext context);
    }
}
