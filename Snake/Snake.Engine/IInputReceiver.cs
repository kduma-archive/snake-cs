using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Engine
{
    public interface IInputReceiver
    {
        bool Has();
        InputAction Get();
    }
}
