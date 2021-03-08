using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Controllers
{
    public class ConsoleMenuItem
    {
        public event Action<ConsoleMenuItem> OnSelected;

        public int CommandNumber { get; set; } = -1;

        public string Text { get; set; }

        public object Tag { get; set; }

        public void Select()
        {
            OnSelected?.Invoke(this);
        }

        public ConsoleMenuItem Select(Action callback)
        {
            OnSelected += _ => callback();
            return this;
        }
    }
}
