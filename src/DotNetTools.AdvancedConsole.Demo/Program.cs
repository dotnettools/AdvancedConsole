using DotNetTools.AdvancedConsole.Controllers;
using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace DotNetTools.AdvancedConsole.Demo
{
    class Program
    {
        private static ConsoleProgram program;
        private static IConsoleScreen screen;
        private static ConsoleScreenRenderer renderer;

        private static void VisitRepo()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/dotnettools/AdvancedConsole",
                UseShellExecute = true,
            });
        }

        internal static IConsoleController CreateMenu()
        {
            return new ConsoleMenuController(screen, renderer)
            {
                FixedWidth = 50,
                HeaderText = "[AdvancedConsole]\r\nDemo Application\nPlease select an option.",
                Items = new List<ConsoleMenuItem>
                {
                    new ConsoleMenuItem { Text = "Test the progressbar" }.Select(() => program.Controller = new TestController(renderer) ),
                    new ConsoleMenuItem { Text = "Visit the repo" }.Select(VisitRepo),
                    new ConsoleMenuItem { Text = "Restart" }.Select(() => program.Controller = CreateMenu() ),
                    new ConsoleMenuItem { Text = "Exit", CommandNumber = 0 }.Select(() => program.Terminate()),
                }
            };
        }

        static void Main(string[] args)
        {
            screen = ConsoleScreen.CreateForWindow();
            renderer = new ConsoleScreenRenderer();
            program = new ConsoleProgram(renderer);
            program.Controller = CreateMenu();
            program.Run();
        }
    }
}
