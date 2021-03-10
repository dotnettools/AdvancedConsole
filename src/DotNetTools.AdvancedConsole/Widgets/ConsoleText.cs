using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    public class ConsoleText : ConsoleWidgetBase
    {
        private string _text;
        private int _height = 1;

        public ConsoleText(ConsoleWidgetHost host) : base(host)
        {
        }

        /// <summary>
        /// The text to display
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                    return;
                _text = value;
                Update();
            }
        }

        /// <summary>
        /// Whether or not to continue to the next row in case of screen overflow.
        /// </summary>
        public bool CharacterWrap { get; set; }

        /// <summary>
        /// if TRUE, the wrapped text will be written in the next row starting from <see cref="ConsoleWidgetBase.Left"/>.
        /// If <see cref="CharacterWrap"/> is FALSE, this property is ignored.
        /// </summary>
        public bool WrapToInitialColumn { get; set; } = true;

        public override int Height { get => _height; set { } }

        protected override void InternalUpdate()
        {
            var lastPosition = UpdateText();

            UpdateColors(lastPosition);
        }

        /// <summary>
        /// Updates characters on the screen.
        /// </summary>
        /// <returns>The position of the final character</returns>
        protected virtual ConsolePoint UpdateText()
        {
            var row = Top;
            var col = Left;
            _height = 1;

            if (Text != null)
                for (var i = 0; i < Text.Length; i++)
                {
                    var ch = Text[i];
                    switch (ch)
                    {
                        case '\r':
                            continue;
                        case '\n':
                            col = Left;
                            row++;
                            _height++;
                            continue;
                    }

                    if (col >= Screen.Columns)
                    {
                        if (CharacterWrap)
                        {
                            col = WrapToInitialColumn ? Left : 0;
                            row++;
                        }
                        else
                            break;
                    }

                    var pixel = Screen.GetPixel(row, col);
                    pixel.BackgroundColor = BackgroundColor;
                    pixel.ForegroundColor = ForegroundColor;
                    pixel.Char = ch;
                    col++;
                }

            return new ConsolePoint(row, col);
        }

        protected virtual void UpdateColors(ConsolePoint lastPosition)
        {
            if (lastPosition.Row == Top)
                for (var i = lastPosition.Column; i < Left + Width; i++)
                {
                    var pixel = Screen.GetPixel(Top, i);
                    pixel.BackgroundColor = BackgroundColor;
                    pixel.ForegroundColor = ForegroundColor;
                }
        }
    }
}
