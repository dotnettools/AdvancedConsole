using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Controllers
{
    public abstract class ConsoleControllerBase : IConsoleController
    {
        public ConsoleControllerBase(IConsoleScreen screen)
        {
            Screen = screen;
            Widgets = new ConsoleWidgetHost(screen);
        }

        public ConsoleControllerBase() : this(ConsoleScreen.CreateForWindow())
        {
        }

        protected ConsoleWidgetHost Widgets { get; }
        protected IConsoleScreen Screen { get; }

        public abstract void Run(ConsoleControllerContext context);
    }
}
