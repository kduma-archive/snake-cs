using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Engine
{
    public class Buffer
    {
        public int Height { get; private set; }
        public int Width { get; private set; }

        private IRenderer ConsoleRenderer { get; set; }
        private readonly List<List<DrawType>> _buffer = new List<List<DrawType>>();

        public Buffer(int width, int height, IRenderer consoleRenderer)
        {
            Height = height;
            ConsoleRenderer = consoleRenderer;
            Width = width;

            Clear();
        }

        public enum DrawType
        {
            Empty, Snake, Point
        }

        public void Clear()
        {
            _buffer.Clear();

            for (var i = 0; i < Height; i++)
            {
                var row = new List<DrawType>(Width);
                for (var j = 0; j < Width; j++)
                {
                    row.Add(DrawType.Empty);
                }
                _buffer.Add(row);
            }

        }

        public void Set(int x, int y)
        {
            Set(x, y, DrawType.Snake);
        }

        public void Set(int x, int y, DrawType draw)
        {
            if (x >= Width || x < 0)
            {
                throw new Exception(string.Format("x is out of range ({0})", x));
            }

            if (y >= Height || y < 0)
            {
                throw new Exception(string.Format("y is out of range ({0})", y));
            }

            _buffer[y][x] = draw;
        }

        public void Draw(string message)
        {
            ConsoleRenderer.Draw(Width, Height, _buffer, message);

        }

        public void Draw(int points, int timeout)
        {
            ConsoleRenderer.Draw(Width, Height, _buffer, points, timeout);
        }
    }
}
