using DotNetTools.AdvancedConsole.Controllers;
using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DotNetTools.AdvancedConsole.Demo
{
    class Program
    {
        private class TestController : IConsoleController
        {
            private IConsoleScreen screen = ConsoleScreen.CreateForWindow();

            public void Run(ConsoleControllerContext context)
            {
                const string StatusText = "Copying Files ({0}%)";
                var host = new ConsoleWidgetHost(screen);
                screen.IsCursorVisible = false;
                var text = host.Add<ConsoleText>(w =>
                {
                    w.Left = 4;
                    w.Top = 2;
                    w.Width = 50;
                    w.BackgroundColor = ConsoleColor.DarkBlue;
                    w.ForegroundColor = ConsoleColor.Yellow;
                    w.Text = string.Format(StatusText, 0);
                });
                var pb = host.Add<ConsoleProgressBar>(p =>
                {
                    p.Left = 4;
                    p.Top = 4;
                    p.Width = 50;
                    p.WrapWithString();
                });
                for (var d = 0d; d <= 1d; d += 0.05)
                {
                    pb.Progress = d;
                    text.Text = string.Format(StatusText, (int)pb.Percent);
                    Thread.Sleep(100);
                }
                pb.Progress = 1;
                text.Text = string.Format(StatusText, 100);
                Console.ReadKey();
                context.Terminate();
            }
        }

        static void Main(string[] args)
        {
            var program = new ConsoleProgram();
            program.Controller = new ConsoleMenuController
            {
                FixedWidth = 50,
                HeaderText = "AdvancedConsole - Please select an option.",
                Items = new List<ConsoleMenuItem>
                {
                    new ConsoleMenuItem { Text = "Take a bow" },
                    new ConsoleMenuItem { Text = "Shoot the messenger" },
                    new ConsoleMenuItem { Text = "Exit", CommandNumber = 0 }.Select(()=> program.Terminate()),
                }
            };
            program.Run();
        }
    }
}
