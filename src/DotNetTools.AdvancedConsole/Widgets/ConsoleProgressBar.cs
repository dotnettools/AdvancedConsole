using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Widgets
{
    public class ConsoleProgressBar : ConsoleWidgetBase
    {
        private double _progress;

        public ConsoleProgressBar(ConsoleWidgetHost host) : base(host)
        {
            Height = 1;
        }

        public override int Height { get => base.Height; set => base.Height = 0; }
        public char EmptyChar { get; set; } = ConsoleBlockChars.LightShade;
        public char FillChar { get; set; } = ConsoleBlockChars.DarkShade;

        /// <summary>
        /// Progress value from 0 to 1
        /// </summary>
        public double Progress
        {
            get => _progress;
            set
            {
                _progress = Math.Clamp(value, 0, 1);
                Update();
            }
        }

        /// <summary>
        /// Progress value as percent
        /// </summary>
        public double Percent
        {
            get => Progress * 100;
            set => Progress = value / 100;
        }

        protected override void InternalUpdate()
        {
            var actualWidth = Math.Min(Width, Screen.Columns - Left);
            var filledCount = (int)(Progress * actualWidth);
            for (var i = 0; i < actualWidth; i++)
            {
                var pixel = Screen.GetPixel(Top, Left + i);
                pixel.Char = i <= filledCount ? FillChar : EmptyChar;
            }
        }

        public ConsoleProgressBar AppendStartString(string str)
        {
            foreach (var ch in str)
            {
                var pixel = Screen.GetPixel(Top, Left++);
                pixel.Char = ch;
                Width--;
            }
            return this;
        }

        public ConsoleProgressBar AppendEndString(string str)
        {
            var i = Left + Width - str.Length;
            Width -= str.Length;
            foreach (var ch in str)
            {
                var pixel = Screen.GetPixel(Top, i++);
                pixel.Char = ch;
            }
            return this;
        }

        public ConsoleProgressBar WrapWithString(string before = "[", string after = "]")
            => AppendStartString(before).AppendEndString(after);
    }
}
