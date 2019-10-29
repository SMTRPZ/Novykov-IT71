using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib.Factory_Method
{
    public abstract class RobotCreator
    {
        abstract public Robot Create(string image);
    }
}
