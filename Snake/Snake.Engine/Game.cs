using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake.Engine
{
    public class Game
    {
        private SnakeDirection _currentDirection = SnakeDirection.Right;
        private int _points = 0;

        private LinkedList<KeyValuePair<int, int>> _snake = new LinkedList<KeyValuePair<int, int>>();
        private List<KeyValuePair<int, int>> _foods = new List<KeyValuePair<int, int>>();
        private readonly Random _random = new Random();

        public int Width { get; private set; }
        public int Height { get; private set; }

        private Buffer Buffer { get; set; }
        private IRenderer Renderer { get; set; }
        private IInputReceiver InputReceiver { get; set; }

        public int Timeout { get; private set; }
        public bool Failed { get; private set; }

        public Game(int width, int height, IRenderer renderer, IInputReceiver inputReceiver)
        {
            Renderer = renderer;
            InputReceiver = inputReceiver;
            Width = width - 2;
            Height = height - 2 - 1;

            Buffer = new Buffer(Width, Height, Renderer);

            Restart();
        }

        public void Restart()
        {
            Failed = false;
            Timeout = 150;
            _points = 0;
            _currentDirection = SnakeDirection.Right;

            _snake.Clear();
            _foods.Clear();

            _snake.AddLast(new KeyValuePair<int, int>(Width / 2, Height / 2));

            for (var i = 0; i < 20; i++)
            {
                _foods.Add(new KeyValuePair<int, int>(_random.Next(0, Width), _random.Next(0, Height)));
            }
        }

        public void RunLoop()
        {
            if (InputReceiver.Has())
            {
                ProcessAction(InputReceiver.Get());
            }

            AdvanceSnakeMove();

            Buffer.Clear();
            foreach (var pair in _foods)
            {
                Buffer.Set(pair.Key, pair.Value, Buffer.DrawType.Point);
            }
            foreach (var pair in _snake)
            {
                Buffer.Set(pair.Key, pair.Value, Buffer.DrawType.Snake);
            }
            Buffer.Draw(_points, Timeout);
        }

        public void RunAfterLoop()
        {
            Buffer.Draw(string.Format("Game Ended. Your Score = {0}", _points));
        }

        public void RunBeforeLoop()
        {
            Buffer.Clear();

            Buffer.Draw("Press any key to start!");
        }

        private void AdvanceSnakeMove()
        {

            var last = _snake.Last.Value;
            var x = last.Key;
            var y = last.Value;

            switch (_currentDirection)
            {
                case SnakeDirection.Up:
                    y--;
                    break;
                case SnakeDirection.Right:
                    x++;
                    break;
                case SnakeDirection.Down:
                    y++;
                    break;
                case SnakeDirection.Left:
                    x--;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (x < 0)
            {
                x = Width - 1;
            }
            if (y < 0)
            {
                y = Height - 1;
            }
            if (x >= Width)
            {
                x = 0;
            }
            if (y >= Height)
            {
                y = 0;
            }

            var first = _snake.First;
            _snake.RemoveFirst();

            Failed = _snake.Any(pair => pair.Key == x && pair.Value == y);

            if (Failed)
            {
                return;
            }

            if (_foods.Any(pair => pair.Key == x && pair.Value == y))
            {
                _points++;
                _foods.RemoveAll(pair => pair.Key == x && pair.Value == y);

                _foods.Add(new KeyValuePair<int, int>(_random.Next(0, Width), _random.Next(0, Height)));

                _snake.AddFirst(first);
            }

            _snake.AddLast(new KeyValuePair<int, int>(x, y));
        }

        private void ProcessAction(InputAction action)
        {
            switch (action)
            {
                case InputAction.Up:
                    if (_currentDirection == SnakeDirection.Left || _currentDirection == SnakeDirection.Right)
                    {
                        _currentDirection = SnakeDirection.Up;
                    }
                    break;

                case InputAction.Down:
                    if (_currentDirection == SnakeDirection.Left || _currentDirection == SnakeDirection.Right)
                    {
                        _currentDirection = SnakeDirection.Down;
                    }
                    break;

                case InputAction.Left:
                    if (_currentDirection == SnakeDirection.Up || _currentDirection == SnakeDirection.Down)
                    {
                        _currentDirection = SnakeDirection.Left;
                    }
                    break;

                case InputAction.Right:
                    if (_currentDirection == SnakeDirection.Up || _currentDirection == SnakeDirection.Down)
                    {
                        _currentDirection = SnakeDirection.Right;
                    }
                    break;

                case InputAction.IncreaseSpeed:
                    Timeout = Math.Min(500, Timeout + 10);
                    break;

                case InputAction.DecreaseSpeed:
                    Timeout = Math.Max(10, Timeout - 10);
                    break;
            }
        }
    }
}
