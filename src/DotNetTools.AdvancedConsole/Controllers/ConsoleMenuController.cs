using DotNetTools.AdvancedConsole.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetTools.AdvancedConsole.Controllers
{
    public class ConsoleMenuController : ConsoleControllerBase
    {
        private ConsoleSelectList _list;

        public ConsoleMenuController(IConsoleScreen screen, ConsoleScreenRenderer renderer) : base(screen, renderer)
        {
        }

        public ConsoleMenuController(IConsoleScreen screen) : base(screen)
        {
        }

        public ConsoleMenuController(ConsoleScreenRenderer renderer) : base(renderer)
        {
        }
        public ConsoleMenuController() : base()
        {
        }

        public string HeaderText { get; set; }

        public IList<ConsoleMenuItem> Items { get; set; }

        public int PaddingTop { get; set; } = 1;

        public int PaddingLeft { get; set; } = 2;

        public int HeaderSpace { get; set; } = 1;

        public int FixedWidth { get; set; } = 0;

        public override void Run(ConsoleControllerContext context)
        {
            Screen.IsCursorVisible = false;
            AdaptItems();
            CreateWidgets();
            AcceptKeys(context);
        }

        private void AdaptItems()
        {
            var commandNumber = 1;
            foreach (var item in Items)
            {
                if (item.CommandNumber < 0)
                    item.CommandNumber = commandNumber;
                commandNumber = Math.Max(item.CommandNumber, commandNumber + 1);
            }
        }

        private void CreateWidgets()
        {
            var row = PaddingTop;
            if (!string.IsNullOrEmpty(HeaderText))
            {
                Widgets.Add<ConsoleText>(t =>
                {
                    t.Left = PaddingLeft;
                    t.Top = row;
                    t.Text = HeaderText;
                });

                row += 1 + HeaderSpace;
            }

            _list = Widgets.Add<ConsoleSelectList>(list =>
            {
                list.Left = PaddingLeft;
                list.Top = row;
                list.Width = FixedWidth;
                foreach (var item in Items)
                    list.Items.Add($"{item.CommandNumber}. {item.Text}");
            });
        }

        private void AcceptKeys(ConsoleControllerContext context)
        {
            while (true)
            {
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        _list.SelectedIndex--;
                        continue;

                    case ConsoleKey.DownArrow:
                        _list.SelectedIndex++;
                        continue;

                    case ConsoleKey.Enter:
                        AcceptItem();
                        return;
                }

                if (char.IsDigit(key.KeyChar))
                {
                    var cmd = int.Parse(key.KeyChar.ToString());
                    var indices = Enumerable.Range(0, Items.Count).Where(i => Items[i].CommandNumber == cmd).ToArray();
                    if (indices.Length == 0)
                        continue;
                    _list.SelectedIndex = indices.First();
                    AcceptItem();
                    return;
                }
            }
        }

        private void AcceptItem()
        {
            var item = Items[_list.SelectedIndex];
            item.Select();
        }
    }
}
