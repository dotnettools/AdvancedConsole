using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    /// <summary>
    /// Provides a list of selectable items.
    /// </summary>
    public class ConsoleSelectList : ConsoleWidgetBase
    {
        private int _selectedIndex;

        public ConsoleSelectList(ConsoleWidgetHost host) : base(host)
        {
        }

        /// <summary>
        /// The items to display
        /// </summary>
        public IList<string> Items { get; } = new List<string>();

        /// <summary>
        /// Index of the selected item, or -1 if none
        /// </summary>
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = Math.Clamp(value, 0, Items.Count - 1);
                Update();
            }
        }

        public ConsoleColor? SelectedBackgroundColor { get; set; } = ConsoleColor.DarkBlue;

        public ConsoleColor? SelectedForegroundColor { get; set; } = ConsoleColor.Yellow;

        protected override void InternalUpdate()
        {
            var width = Width > 0 ? Width : Items.Max(s => s.Length);

            for (var i = 0; i < Items.Count; i++)
            {
                var row = Top + i;
                var item = Items[i];
                var selected = i == SelectedIndex;
                var bg = selected ? SelectedBackgroundColor : BackgroundColor;
                var fg = selected ? SelectedForegroundColor : ForegroundColor;
                Screen.Write(item, row, Left);
                Screen.Paint(row, row, Left, Left + width - 1, bg, fg);
            }
        }
    }
}
