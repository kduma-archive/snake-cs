using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Snake.Engine;

namespace Snake.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(Console.WindowWidth, Console.WindowHeight, new ConsoleRenderer(), new ConsoleInputReceiver());

            game.RunBeforeLoop();

            while (!game.Failed)
            {
                Thread.Sleep(game.Timeout);

                game.RunLoop();
            }

            game.RunAfterLoop();
        }
    }
}
