using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    public abstract class ConsoleWidgetBase : IConsoleWidget
    {
        protected ConsoleWidgetBase(ConsoleWidgetHost host)
        {
            Host = host;
        }

        public bool IsVisible { get; set; } = true;
        public virtual int Left { get; set; }
        public virtual int Top { get; set; }
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }
        public ConsoleColor? BackgroundColor { get; set; }
        public ConsoleColor? ForegroundColor { get; set; }
        protected ConsoleWidgetHost Host { get; }
        protected IConsoleScreen Screen => Host.Screen;

        protected abstract void InternalUpdate();

        public void Update()
        {
            if (!IsVisible)
                return;
            InternalUpdate();
            Host.Render();
        }
    }
}
