using DotNetTools.AdvancedConsole.Controllers;
using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DotNetTools.AdvancedConsole.Demo
{
    public class TestController : ConsoleControllerBase
    {
        public TestController(ConsoleScreenRenderer renderer) : base(renderer)
        {
        }

        public override void Run(ConsoleControllerContext context)
        {
            const string StatusText = "Copying Files ({0}%)";
            var host = new ConsoleWidgetHost(Screen, Renderer);
            Screen.IsCursorVisible = false;
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
            context.NextController = Program.CreateMenu();
            Console.ReadKey(true);
        }
    }
}
