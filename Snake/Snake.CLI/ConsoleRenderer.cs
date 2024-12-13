using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snake.Engine;
using Buffer = Snake.Engine.Buffer;

namespace Snake.CLI
{
    class ConsoleRenderer: IRenderer
    {
        public void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, string message)
        {
            Console.Clear();

            var toPrint = new StringBuilder(width + 2);
            toPrint.Append('╔');
            toPrint.Append('═', width);
            toPrint.Append('╗');
            Console.Write(toPrint.ToString());

            var rowToPrint = height / 2;
            for (var index = 0; index < buffer.Count; index++)
            {
                var row = buffer[index];
                toPrint = new StringBuilder(width + 2);
                toPrint.Append('║');

                if (index == rowToPrint)
                {
                    var whiteCount = width - message.Length;
                    toPrint.Append(' ', whiteCount / 2);
                    toPrint.Append(message);
                    toPrint.Append(' ', whiteCount / 2 + whiteCount % 2);
                }
                else
                {
                    toPrint.Append(' ', width);
                }
                toPrint.Append('║');
                Console.Write(toPrint.ToString());
            }

            toPrint = new StringBuilder(width + 2);
            toPrint.Append('╚');
            toPrint.Append('═', width);
            toPrint.Append('╝');
            Console.Write(toPrint.ToString());

            Console.ReadKey(true);
        }

        public void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, int points, int timeout)
        {
            Console.Clear();

            var toPrint = new StringBuilder(width + 2);
            toPrint.Append('╔');
            toPrint.Append('═', width);
            toPrint.Append('╗');
            Console.Write(toPrint.ToString());

            foreach (var row in buffer)
            {
                toPrint = new StringBuilder(width + 2);
                toPrint.Append('║');
                foreach (var bit in row)
                {
                    switch (bit)
                    {
                        case Buffer.DrawType.Empty:
                            toPrint.Append(" ");
                            break;
                        case Buffer.DrawType.Snake:
                            toPrint.Append("█");
                            break;
                        case Buffer.DrawType.Point:
                            toPrint.Append("X");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                toPrint.Append('║');
                Console.Write(toPrint.ToString());
            }

            toPrint = new StringBuilder(width + 2);
            toPrint.Append('╚');
            toPrint.Append('═', width);
            toPrint.Append('╝');
            Console.Write(toPrint.ToString());

            Console.Write("Points = {0}, speed = {1}", points, timeout);
        }
    }
}
