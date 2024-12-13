using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Snake.Engine;
using Buffer = Snake.Engine.Buffer;

namespace Snake.GUI
{
    class WinFormsRenderer : IRenderer
    {
        private int _width;
        private int _height;
        private List<List<Buffer.DrawType>> _buffer;
        private int _points;
        private int _timeout;
        private string _message;
        public Snake Snake { get; set; }

        public WinFormsRenderer(Snake snake)
        {
            Snake = snake;
            Snake.Panel.Paint += SnakeOnPaint;
        }

        private void SnakeOnPaint(object sender, PaintEventArgs e)
        {
            var panel = (Panel) sender;

            var w = panel.Width / _width;
            var h = panel.Height / _height;


            for (var i = 0; i < _buffer.Count; i++)
            {
                for (var j = 0; j < _buffer[i].Count; j++)
                {
                    switch (_buffer[i][j])
                    {
                        case Buffer.DrawType.Snake:
                            e.Graphics.FillRectangle(new SolidBrush(Color.Black), j * w + 2, i * h + 2, w - 4, h - 4);
                            break;
                        case Buffer.DrawType.Point:
                            e.Graphics.FillRectangle(new SolidBrush(Color.Red), j * w, i * h, w, h);
                            break;
                    }
                }
            }
        }

        public void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, string message)
        {
            _message = message;
            _timeout = 0;
            _points = 0;

            _buffer = buffer;
            _height = height;
            _width = width;

            MessageBox.Show(message, "Snake Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, int points, int timeout)
        {
            _message = "";
            _timeout = timeout;
            _points = points;

            _buffer = buffer;
            _height = height;
            _width = width;

            Snake.Panel.Invalidate();
            Snake.Speed = timeout;
            Snake.Score = points;
        }
    }
}