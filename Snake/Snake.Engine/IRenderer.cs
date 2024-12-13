using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Engine
{
    public interface IRenderer
    {
        void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, string message);

        void Draw(int width, int height, List<List<Buffer.DrawType>> buffer, int points, int timeout);
    }
}
