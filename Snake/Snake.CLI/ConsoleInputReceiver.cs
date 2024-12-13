using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Engine;

namespace Snake.CLI
{
    class ConsoleInputReceiver : IInputReceiver
    {
        public bool Has()
        {
            return Console.KeyAvailable;
        }

        public InputAction Get()
        {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    return InputAction.Up;

                case ConsoleKey.DownArrow:
                    return InputAction.Down;

                case ConsoleKey.LeftArrow:
                    return InputAction.Left;

                case ConsoleKey.RightArrow:
                    return InputAction.Right;
                    break;

                case ConsoleKey.PageUp:
                    return InputAction.IncreaseSpeed;

                case ConsoleKey.PageDown:
                    return InputAction.DecreaseSpeed;
            }

            return InputAction.None;
        }
    }
}
