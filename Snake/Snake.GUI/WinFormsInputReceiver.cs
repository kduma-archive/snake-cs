using System;
using System.Windows.Forms;
using Snake.Engine;

namespace Snake.GUI
{
    class WinFormsInputReceiver : IInputReceiver
    {
        public Snake Snake { get; set; }

        public InputAction LastAction { get; set; }

        public WinFormsInputReceiver(Snake snake)
        {
            Snake = snake;

            Snake.KeyDown += SnakeOnKeyDown;
        }

        private void SnakeOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Up:
                    LastAction = InputAction.Up;
                    break;
                case Keys.Down:
                    LastAction = InputAction.Down;
                    break;
                case Keys.Left:
                    LastAction = InputAction.Left;
                    break;
                case Keys.Right:
                    LastAction = InputAction.Right;
                    break;
                case Keys.PageUp:
                    LastAction = InputAction.IncreaseSpeed;
                    break;
                case Keys.PageDown:
                    LastAction = InputAction.DecreaseSpeed;
                    break;
            }
        }

        public bool Has()
        {
            return LastAction != InputAction.None;
        }

        public InputAction Get()
        {
            var action = LastAction;
            LastAction = InputAction.None;

            return action;
        }
    }
}