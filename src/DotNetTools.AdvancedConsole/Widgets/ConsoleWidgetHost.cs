using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    public class ConsoleWidgetHost
    {
        private readonly List<IConsoleWidget> _widgets = new List<IConsoleWidget>();
        private ConsoleScreenRenderer _renderer;

        public ConsoleWidgetHost(IConsoleScreen screen, Action<ConsoleWidgetHost> renderer)
        {
            Screen = screen;
            Renderer = renderer;
        }

        public ConsoleWidgetHost(IConsoleScreen screen, ConsoleScreenRenderer renderer)
        {
            Screen = screen;
            Renderer = _ =>
            {
                _renderer = renderer;
                RenderDefault();
            };
        }

        public ConsoleWidgetHost(IConsoleScreen screen)
        {
            Screen = screen;
            Renderer = _ => RenderDefault();
        }

        public IConsoleScreen Screen { get; }
        public Action<ConsoleWidgetHost> Renderer { get; }

        public void Add(IConsoleWidget widget)
        {
            _widgets.Add(widget);
        }

        public void Remove(IConsoleWidget widget)
        {
            _widgets.Remove(widget);
        }

        public T Add<T>() where T : ConsoleWidgetBase
        {
            var widget = (T)Activator.CreateInstance(typeof(T), this);
            Add(widget);
            return widget;
        }

        public T Add<T>(Action<T> modifier) where T : ConsoleWidgetBase
        {
            var widget = Add<T>();
            modifier(widget);
            widget.Update();
            return widget;
        }

        public void Update()
        {
            Screen.ResizeScreenOnDemand();

            foreach (var widget in _widgets)
                widget.Update();
        }

        public void Render()
        {
            Renderer?.Invoke(this);
        }

        private void RenderDefault()
        {
            if (_renderer == null)
                _renderer = new ConsoleScreenRenderer();
            _renderer.Render(Screen, new ConsoleRenderOptions
            {
                RenderingMode = ConsoleRenderingMode.Update,
            });
        }
    }
}
